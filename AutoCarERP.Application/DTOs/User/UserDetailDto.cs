namespace AutoCarERP.Application.DTOs.User;

public record UserDetailDto(
    string Id,
    string Email,
    string UserName,
    bool EmailConfirmed,
    bool IsActive,
    string Role,
    IReadOnlyList<string> Permissions,
    DateTime? CreatedAt,
    DateTime? LastLogin);

