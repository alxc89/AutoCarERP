namespace AutoCarERP.Application.DTOs.Role;

public record CreateRoleDto(string Name, IReadOnlyList<string> Permissions);

