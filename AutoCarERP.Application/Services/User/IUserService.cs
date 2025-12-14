using AutoCarERP.Application.Common;
using AutoCarERP.Application.DTOs;
using AutoCarERP.Application.DTOs.User;

namespace AutoCarERP.Application.Services.User;

public interface IUserService
{
    Task<PagedResult<UserListDto>> ListUsersAsync(
        string? search,
        int page,
        int pageSize,
        bool includeInactive,
        CancellationToken ct = default);

    Task<UserDetailDto?> GetUserByIdAsync(string userId, CancellationToken ct = default);

    Task<ServiceResult<string>> CreateUserAsync(CreateUserDto dto, CancellationToken ct = default);

    Task<ServiceResult> UpdateUserRoleAsync(string userId, UpdateUserRoleDto dto, CancellationToken ct = default);

    Task<ServiceResult> UpdateUserPermissionsAsync(
        string userId,
        UpdateUserPermissionsDto dto,
        CancellationToken ct = default);

    Task<ServiceResult> DeactivateUserAsync(string userId, CancellationToken ct = default);
    Task<ServiceResult> ActivateUserAsync(string userId, CancellationToken ct = default);
    Task<ServiceResult> DeleteUserAsync(string userId, CancellationToken ct = default);

    Task<bool> UpdateProfileAsync(string userId, UpdateProfileDto dto, CancellationToken ct = default);
    Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto dto, CancellationToken ct = default);
    Task UpdatePreferencesAsync(string userId, UserPreferencesDto dto, CancellationToken ct = default);
    Task<UserPreferencesDto> GetPreferencesAsync(string userId, CancellationToken ct = default);
}
