using Microsoft.AspNetCore.Identity;

namespace AutoCarERP.API.Services;

public record TokenResult(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);

public interface ITokenService
{
    Task<TokenResult> GenerateTokensAsync(IdentityUser user, CancellationToken ct = default);
    Task<IdentityUser?> ValidateRefreshTokenAsync(string refreshToken, CancellationToken ct = default);
    Task InvalidateRefreshTokenAsync(string refreshToken, CancellationToken ct = default);
}
