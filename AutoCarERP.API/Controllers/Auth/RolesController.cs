using AutoCarERP.API.Models.Role;
using AutoCarERP.Application.DTOs.Role;
using AutoCarERP.Application.Services.Role;
using AutoCarERP.Core.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoCarERP.API.Controllers.Auth;

[ApiController]
[Route("api/v1/Auth/roles")]
[Authorize]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Perfil.Read)]
    public async Task<IActionResult> ListAsync(CancellationToken ct)
    {
        var result = await _roleService.ListAsync(ct);
        return Ok(result);
    }

    [HttpGet("{name}")]
    [Authorize(Policy = Permissions.Perfil.Read)]
    public async Task<IActionResult> GetAsync([FromRoute] string name, CancellationToken ct)
    {
        var role = await _roleService.GetAsync(name, ct);
        return role is null ? NotFound(new { message = "Perfil não encontrado." }) : Ok(role);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Perfil.Create)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRoleRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _roleService.CreateAsync(
            new CreateRoleDto(request.Name, request.Permissions),
            ct);

        if (!result.Success)
            return BadRequest(new { message = result.Error });

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{name}/permissions")]
    [Authorize(Policy = Permissions.Perfil.Edit)]
    public async Task<IActionResult> UpdatePermissionsAsync(
        [FromRoute] string name,
        [FromBody] UpdateRolePermissionsRequest request,
        CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _roleService.UpdatePermissionsAsync(
            name,
            new UpdateRolePermissionsDto(request.Permissions),
            ct);

        if (!result.Success)
        {
            if (result.Error == "Perfil não encontrado.")
                return NotFound(new { message = result.Error });
            return BadRequest(new { message = result.Error });
        }

        return Ok(new { message = "Permissões do perfil atualizadas com sucesso" });
    }

    [HttpDelete("{name}")]
    [Authorize(Policy = Permissions.Perfil.Delete)]
    public async Task<IActionResult> DeleteAsync([FromRoute] string name, CancellationToken ct)
    {
        var result = await _roleService.DeleteAsync(name, ct);
        if (!result.Success)
        {
            if (result.Error == "Perfil não encontrado.")
                return NotFound(new { message = result.Error });
            return BadRequest(new { message = result.Error });
        }

        return NoContent();
    }
}

