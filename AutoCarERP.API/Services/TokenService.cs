using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoCarERP.API.Options;
using AutoCarERP.Core.Auth;
using AutoCarERP.Core.Entities;
using AutoCarERP.Infra.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AutoCarERP.API.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtOptions _options;
    private readonly AppDbContext _context;

    public TokenService(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<JwtOptions> options,
        AppDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _options = options.Value;
        _context = context;
    }

    public async Task<TokenResult> GenerateTokensAsync(IdentityUser user, CancellationToken ct = default)
    {
        var accessToken = await GenerateAccessTokenAsync(user);
        var refreshToken = await CreateRefreshTokenAsync(user, ct);

        return new TokenResult(
            accessToken.token,
            accessToken.expiresAt,
            refreshToken.Token,
            refreshToken.ExpiresAt);
    }

    public async Task<IdentityUser?> ValidateRefreshTokenAsync(string refreshToken, CancellationToken ct = default)
    {
        var token = await _context.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Token == refreshToken, ct);

        if (token is null || !token.IsActive)
            return null;

        return await _userManager.FindByIdAsync(token.UserId);
    }

    public async Task InvalidateRefreshTokenAsync(string refreshToken, CancellationToken ct = default)
    {
        var token = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken, ct);
        if (token is null) return;

        token.RevokedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(ct);
    }

    private async Task<(string token, DateTime expiresAt)> GenerateAccessTokenAsync(IdentityUser user)
    {
        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(_options.AccessTokenMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName ?? string.Empty)
        };

        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var roleName in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, roleName));
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null) continue;
            var roleClaims = await _roleManager.GetClaimsAsync(role);
            claims.AddRange(roleClaims.Where(c => c.Type == Permissions.ClaimType));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: now,
            expires: expires,
            signingCredentials: creds);

        return (new JwtSecurityTokenHandler().WriteToken(token), expires);
    }

    private async Task<RefreshToken> CreateRefreshTokenAsync(IdentityUser user, CancellationToken ct)
    {
        var tokenString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = tokenString,
            ExpiresAt = DateTime.UtcNow.AddDays(_options.RefreshTokenDays)
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync(ct);

        return refreshToken;
    }
}
