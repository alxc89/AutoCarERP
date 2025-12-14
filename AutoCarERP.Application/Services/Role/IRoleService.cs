using AutoCarERP.Application.Common;
using AutoCarERP.Application.DTOs.Role;

namespace AutoCarERP.Application.Services.Role;

public interface IRoleService
{
    Task<IReadOnlyList<RoleListDto>> ListAsync(CancellationToken ct = default);
    Task<RoleDetailDto?> GetAsync(string name, CancellationToken ct = default);
    Task<ServiceResult> CreateAsync(CreateRoleDto dto, CancellationToken ct = default);
    Task<ServiceResult> UpdatePermissionsAsync(string name, UpdateRolePermissionsDto dto, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(string name, CancellationToken ct = default);
}

