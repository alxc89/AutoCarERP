using AutoCarERP.Application.DTOs.User;

namespace AutoCarERP.Application.Services.User;

public interface IUserService
{
    Task<bool> UpdateProfileAsync(string userId, UpdateProfileDto dto, CancellationToken ct = default);
    Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto dto, CancellationToken ct = default);
    Task UpdatePreferencesAsync(string userId, UserPreferencesDto dto, CancellationToken ct = default);
    Task<UserPreferencesDto> GetPreferencesAsync(string userId, CancellationToken ct = default);
}
