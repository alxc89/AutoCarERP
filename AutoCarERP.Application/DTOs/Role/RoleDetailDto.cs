namespace AutoCarERP.Application.DTOs.Role;

public record RoleDetailDto(string Name, int UserCount, IReadOnlyList<string> Permissions, bool IsSystem);

