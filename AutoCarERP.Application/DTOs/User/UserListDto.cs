namespace AutoCarERP.Application.DTOs.User;

public record UserListDto(
    string Id,
    string Email,
    string UserName,
    bool EmailConfirmed,
    bool IsActive,
    string Role,
    IReadOnlyList<string> Permissions);

