using AutoCarERP.Application.Common;
using AutoCarERP.Application.DTOs.Role;
using AutoCarERP.Application.Services.Role;
using AutoCarERP.Core.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AutoCarERP.Infra.Services.Role;

public class RoleService : IRoleService
{
    private const string AdminRoleName = "ADMIN";

    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<IReadOnlyList<RoleListDto>> ListAsync(CancellationToken ct = default)
    {
        var roles = await _roleManager.Roles.AsNoTracking().OrderBy(r => r.Name).ToListAsync(ct);
        var result = new List<RoleListDto>(roles.Count);

        foreach (var role in roles)
        {
            var name = role.Name ?? string.Empty;
            var users = string.IsNullOrWhiteSpace(name) ? [] : await _userManager.GetUsersInRoleAsync(name);
            result.Add(new RoleListDto(name, users.Count));
        }

        return result;
    }

    public async Task<RoleDetailDto?> GetAsync(string name, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;
        var normalized = name.Trim().ToUpperInvariant();

        var role = await _roleManager.FindByNameAsync(normalized);
        if (role is null) return null;

        var users = await _userManager.GetUsersInRoleAsync(normalized);
        var claims = await _roleManager.GetClaimsAsync(role);
        var permissions = claims
            .Where(c => c.Type == Permissions.ClaimType)
            .Select(c => c.Value)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(p => p)
            .ToArray();

        var isSystem = normalized == AdminRoleName || normalized == "USER" || normalized == "MANAGER";

        return new RoleDetailDto(normalized, users.Count, permissions, isSystem);
    }

    public async Task<ServiceResult> CreateAsync(CreateRoleDto dto, CancellationToken ct = default)
    {
        var name = (dto.Name ?? string.Empty).Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(name))
            return ServiceResult.Fail("Nome do perfil é obrigatório.");

        if (name == AdminRoleName)
            return ServiceResult.Fail("Não é permitido criar um perfil com este nome.");

        var exists = await _roleManager.RoleExistsAsync(name);
        if (exists)
            return ServiceResult.Fail("Perfil já existe.");

        var permissions = (dto.Permissions ?? Array.Empty<string>())
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Select(p => p.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var invalidPermissions = permissions.Where(p => !Permissions.All.Contains(p)).ToArray();
        if (invalidPermissions.Length > 0)
            return ServiceResult.Fail($"Permissões inválidas: {string.Join(", ", invalidPermissions)}");

        var create = await _roleManager.CreateAsync(new IdentityRole(name));
        if (!create.Succeeded)
            return ServiceResult.Fail(string.Join(", ", create.Errors.Select(e => e.Description)));

        if (permissions.Length > 0)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role is null)
                return ServiceResult.Fail("Falha ao localizar o perfil recém-criado.");

            foreach (var permission in permissions)
            {
                var add = await _roleManager.AddClaimAsync(
                    role,
                    new Claim(Permissions.ClaimType, permission));
                if (!add.Succeeded)
                    return ServiceResult.Fail(string.Join(", ", add.Errors.Select(e => e.Description)));
            }
        }

        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> UpdatePermissionsAsync(string name, UpdateRolePermissionsDto dto, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ServiceResult.Fail("Perfil inválido.");

        var normalized = name.Trim().ToUpperInvariant();
        if (normalized == AdminRoleName)
            return ServiceResult.Fail("Não é permitido ajustar permissões do perfil ADMIN.");

        var role = await _roleManager.FindByNameAsync(normalized);
        if (role is null)
            return ServiceResult.Fail("Perfil não encontrado.");

        var permissions = (dto.Permissions ?? Array.Empty<string>())
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Select(p => p.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var invalidPermissions = permissions.Where(p => !Permissions.All.Contains(p)).ToArray();
        if (invalidPermissions.Length > 0)
            return ServiceResult.Fail($"Permissões inválidas: {string.Join(", ", invalidPermissions)}");

        var claims = await _roleManager.GetClaimsAsync(role);
        var currentPermissionClaims = claims.Where(c => c.Type == Permissions.ClaimType).ToList();

        if (currentPermissionClaims.Count > 0)
        {
            foreach (var claim in currentPermissionClaims)
            {
                var remove = await _roleManager.RemoveClaimAsync(role, claim);
                if (!remove.Succeeded)
                    return ServiceResult.Fail(string.Join(", ", remove.Errors.Select(e => e.Description)));
            }
        }

        foreach (var permission in permissions)
        {
            var add = await _roleManager.AddClaimAsync(role, new Claim(Permissions.ClaimType, permission));
            if (!add.Succeeded)
                return ServiceResult.Fail(string.Join(", ", add.Errors.Select(e => e.Description)));
        }

        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> DeleteAsync(string name, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ServiceResult.Fail("Perfil inválido.");

        var normalized = name.Trim().ToUpperInvariant();
        if (normalized == AdminRoleName)
            return ServiceResult.Fail("Não é permitido deletar o perfil ADMIN.");

        var role = await _roleManager.FindByNameAsync(normalized);
        if (role is null)
            return ServiceResult.Fail("Perfil não encontrado.");

        var users = await _userManager.GetUsersInRoleAsync(normalized);
        if (users.Count > 0)
            return ServiceResult.Fail("Não é possível deletar um perfil que ainda possui usuários.");

        var delete = await _roleManager.DeleteAsync(role);
        if (!delete.Succeeded)
            return ServiceResult.Fail(string.Join(", ", delete.Errors.Select(e => e.Description)));

        return ServiceResult.Ok();
    }
}
