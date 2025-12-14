using AutoCarERP.Application.Common;
using AutoCarERP.Application.DTOs;
using AutoCarERP.Application.DTOs.User;
using AutoCarERP.Application.Services.User;
using AutoCarERP.Core.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCarERP.Infra.Services.User;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<PagedResult<UserListDto>> ListUsersAsync(
        string? search,
        int page,
        int pageSize,
        bool includeInactive,
        CancellationToken ct = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _userManager.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(u =>
                (u.Email != null && u.Email.ToLower().Contains(term)) ||
                (u.UserName != null && u.UserName.ToLower().Contains(term)));
        }

        if (!includeInactive)
        {
            var now = DateTimeOffset.UtcNow;
            query = query.Where(u => u.LockoutEnd == null || u.LockoutEnd <= now);
        }

        var total = await query.CountAsync(ct);
        var users = await query
            .OrderBy(u => u.Email)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        var items = new List<UserListDto>(users.Count);
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = SelectPrimaryRole(roles);
            var permissions = await GetCustomPermissionsAsync(user);

            items.Add(new UserListDto(
                user.Id,
                user.Email ?? string.Empty,
                user.UserName ?? string.Empty,
                user.EmailConfirmed,
                IsActive(user),
                role,
                permissions));
        }

        return new PagedResult<UserListDto>(items, page, pageSize, total);
    }

    public async Task<UserDetailDto?> GetUserByIdAsync(string userId, CancellationToken ct = default)
    {
        var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId, ct);
        if (user is null) return null;

        var roles = await _userManager.GetRolesAsync(user);
        var role = SelectPrimaryRole(roles);
        var permissions = await GetCustomPermissionsAsync(user);

        return new UserDetailDto(
            user.Id,
            user.Email ?? string.Empty,
            user.UserName ?? string.Empty,
            user.EmailConfirmed,
            IsActive(user),
            role,
            permissions,
            CreatedAt: null,
            LastLogin: null);
    }

    public async Task<ServiceResult<string>> CreateUserAsync(CreateUserDto dto, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Email))
            return ServiceResult<string>.Fail("Email é obrigatório.");

        var role = (dto.Role ?? string.Empty).Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(role))
            return ServiceResult<string>.Fail("Perfil inválido.");

        if (!await _roleManager.RoleExistsAsync(role))
            return ServiceResult<string>.Fail("Perfil não encontrado.");

        var permissions = (dto.Permissions ?? Array.Empty<string>())
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Select(p => p.Trim())
            .Distinct()
            .ToArray();

        var invalidPermissions = permissions.Where(p => !Permissions.All.Contains(p)).ToArray();
        if (invalidPermissions.Length > 0)
            return ServiceResult<string>.Fail($"Permissões inválidas: {string.Join(", ", invalidPermissions)}");

        var existing = await _userManager.FindByEmailAsync(dto.Email);
        if (existing is not null)
            return ServiceResult<string>.Fail("Usuário já existe.");

        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            EmailConfirmed = true
        };

        var create = await _userManager.CreateAsync(user, dto.Password);
        if (!create.Succeeded)
            return ServiceResult<string>.Fail(string.Join(", ", create.Errors.Select(e => e.Description)));

        var addRole = await _userManager.AddToRoleAsync(user, role);
        if (!addRole.Succeeded)
            return ServiceResult<string>.Fail(string.Join(", ", addRole.Errors.Select(e => e.Description)));

        if (permissions.Length > 0)
        {
            var addClaims = await _userManager.AddClaimsAsync(
                user,
                permissions.Select(p => new Claim(Permissions.ClaimType, p)));
            if (!addClaims.Succeeded)
                return ServiceResult<string>.Fail(string.Join(", ", addClaims.Errors.Select(e => e.Description)));
        }

        return ServiceResult<string>.Ok(user.Id);
    }

    public async Task<ServiceResult> UpdateUserRoleAsync(string userId, UpdateUserRoleDto dto, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return ServiceResult.Fail("Usuário não encontrado.");

        var newRole = (dto.Role ?? string.Empty).Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(newRole))
            return ServiceResult.Fail("Perfil inválido.");

        if (!await _roleManager.RoleExistsAsync(newRole))
            return ServiceResult.Fail("Perfil não encontrado.");

        var currentRoles = await _userManager.GetRolesAsync(user);
        var isAdmin = currentRoles.Contains("ADMIN");

        if (isAdmin && newRole != "ADMIN" && await IsLastAdminAsync(user))
            return ServiceResult.Fail("Não é possível alterar o último administrador do sistema.");

        if (currentRoles.Count > 0)
        {
            var remove = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!remove.Succeeded)
                return ServiceResult.Fail(string.Join(", ", remove.Errors.Select(e => e.Description)));
        }

        var add = await _userManager.AddToRoleAsync(user, newRole);
        if (!add.Succeeded)
            return ServiceResult.Fail(string.Join(", ", add.Errors.Select(e => e.Description)));

        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> UpdateUserPermissionsAsync(
        string userId,
        UpdateUserPermissionsDto dto,
        CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return ServiceResult.Fail("Usuário não encontrado.");

        var permissions = (dto.Permissions ?? Array.Empty<string>())
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Select(p => p.Trim())
            .Distinct()
            .ToArray();

        var invalidPermissions = permissions.Where(p => !Permissions.All.Contains(p)).ToArray();
        if (invalidPermissions.Length > 0)
            return ServiceResult.Fail($"Permissões inválidas: {string.Join(", ", invalidPermissions)}");

        var claims = await _userManager.GetClaimsAsync(user);
        var currentPermissionClaims = claims.Where(c => c.Type == Permissions.ClaimType).ToList();

        if (currentPermissionClaims.Count > 0)
        {
            var remove = await _userManager.RemoveClaimsAsync(user, currentPermissionClaims);
            if (!remove.Succeeded)
                return ServiceResult.Fail(string.Join(", ", remove.Errors.Select(e => e.Description)));
        }

        if (permissions.Length > 0)
        {
            var add = await _userManager.AddClaimsAsync(user, permissions.Select(p => new Claim(Permissions.ClaimType, p)));
            if (!add.Succeeded)
                return ServiceResult.Fail(string.Join(", ", add.Errors.Select(e => e.Description)));
        }

        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> DeactivateUserAsync(string userId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return ServiceResult.Fail("Usuário não encontrado.");

        if (await IsLastAdminAsync(user))
            return ServiceResult.Fail("Não é possível desativar o último administrador do sistema.");

        var lockoutEnabled = await _userManager.SetLockoutEnabledAsync(user, true);
        if (!lockoutEnabled.Succeeded)
            return ServiceResult.Fail(string.Join(", ", lockoutEnabled.Errors.Select(e => e.Description)));

        var result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
        if (!result.Succeeded)
            return ServiceResult.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));

        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> ActivateUserAsync(string userId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return ServiceResult.Fail("Usuário não encontrado.");

        var result = await _userManager.SetLockoutEndDateAsync(user, null);
        if (!result.Succeeded)
            return ServiceResult.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));

        await _userManager.ResetAccessFailedCountAsync(user);
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> DeleteUserAsync(string userId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return ServiceResult.Fail("Usuário não encontrado.");

        if (IsActive(user))
            return ServiceResult.Fail("Não é possível excluir um usuário ativo. Desative-o antes.");

        if (await IsLastAdminAsync(user))
            return ServiceResult.Fail("Não é possível excluir o último administrador do sistema.");

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            return ServiceResult.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));

        return ServiceResult.Ok();
    }

    public async Task<bool> UpdateProfileAsync(string userId, UpdateProfileDto dto, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        user.UserName = dto.Nome;
        user.Email = dto.Email;

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto dto, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        var result = await _userManager.ChangePasswordAsync(user, dto.SenhaAtual, dto.NovaSenha);
        return result.Succeeded;
    }

    public async Task UpdatePreferencesAsync(string userId, UserPreferencesDto dto, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new Exception("Usuário não encontrado");

        var existingClaims = await _userManager.GetClaimsAsync(user);
        
        var preferenceClaims = existingClaims.Where(c => 
            c.Type == "Tema" || c.Type == "Idioma" || c.Type == "NotificarOsAtrasada").ToList();
        
        if (preferenceClaims.Any())
            await _userManager.RemoveClaimsAsync(user, preferenceClaims);

        await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Tema", dto.Tema));
        await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Idioma", dto.Idioma));
        await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("NotificarOsAtrasada", dto.NotificarOsAtrasada.ToString()));
    }

    public async Task<UserPreferencesDto> GetPreferencesAsync(string userId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new Exception("Usuário não encontrado");

        var claims = await _userManager.GetClaimsAsync(user);
        
        return new UserPreferencesDto
        {
            Tema = claims.FirstOrDefault(c => c.Type == "Tema")?.Value ?? "light",
            Idioma = claims.FirstOrDefault(c => c.Type == "Idioma")?.Value ?? "pt-BR",
            NotificarOsAtrasada = bool.Parse(claims.FirstOrDefault(c => c.Type == "NotificarOsAtrasada")?.Value ?? "true")
        };
    }

    private static bool IsActive(IdentityUser user)
    {
        var now = DateTimeOffset.UtcNow;
        return user.LockoutEnd is null || user.LockoutEnd <= now;
    }

    private static string SelectPrimaryRole(IEnumerable<string> roles)
    {
        var list = roles as IReadOnlyCollection<string> ?? roles.ToArray();

        if (list.Contains("ADMIN")) return "ADMIN";
        if (list.Contains("MANAGER")) return "MANAGER";
        if (list.Contains("USER")) return "USER";
        return list.FirstOrDefault() ?? "USER";
    }

    private async Task<bool> IsLastAdminAsync(IdentityUser user)
    {
        if (!await _userManager.IsInRoleAsync(user, "ADMIN"))
            return false;

        var admins = await _userManager.GetUsersInRoleAsync("ADMIN");
        return admins.Count == 1 && admins[0].Id == user.Id;
    }

    private async Task<IReadOnlyList<string>> GetCustomPermissionsAsync(IdentityUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        return userClaims
            .Where(c => c.Type == Permissions.ClaimType)
            .Select(c => c.Value)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(p => p)
            .ToArray();
    }

    // Roles são gerenciadas via RoleService/RoleManager (exceto ADMIN/MANAGER/USER via seeder).
}
