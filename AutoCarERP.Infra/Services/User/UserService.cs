using AutoCarERP.Application.DTOs.User;
using AutoCarERP.Application.Services.User;
using Microsoft.AspNetCore.Identity;

namespace AutoCarERP.Infra.Services.User;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
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
}
