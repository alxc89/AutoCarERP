namespace AutoCarERP.Application.DTOs.User;

public record CreateUserDto(
    string Email,
    string Password,
    string Role,
    IReadOnlyList<string>? Permissions);

