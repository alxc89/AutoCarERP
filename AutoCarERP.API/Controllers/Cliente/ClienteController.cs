using AutoCarERP.Application.DTOs.Cliente;
using AutoCarERP.Application.Services.Cliente;
using AutoCarERP.Core.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoCarERP.API.Controllers.Cliente;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class ClienteController(IClienteService clienteService) : ControllerBase
{
    private readonly IClienteService _clienteService = clienteService;

    [HttpPost("create")]
    [Authorize(Policy = Permissions.Cliente.Create)]
    public async Task<IActionResult> CreateAsync([FromBody] ClienteCreateDto req, CancellationToken ct)
    {
        try
        {
            var cliente = await _clienteService
                .CreateAsync(req, ct);
            return StatusCode(StatusCodes.Status201Created, cliente);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("get-by-cod/{cod}")]
    [Authorize(Policy = Permissions.Cliente.Read)]
    public async Task<IActionResult> GetByIdAsync(int cod, CancellationToken ct)
    {
        try
        {
            var cliente = await _clienteService
                .GetByIdAsync(cod, ct);
            return StatusCode(StatusCodes.Status200OK, cliente);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("get-all")]
    [Authorize(Policy = Permissions.Cliente.Read)]
    public async Task<IActionResult> ListAsync(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default
    )
    {
        try
        {
            var lista = await _clienteService
                .ListAsync(search, page, pageSize, ct);

            return StatusCode(StatusCodes.Status200OK, lista);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("update/{cod}")]
    [Authorize(Policy = Permissions.Cliente.Create)]
    public async Task<IActionResult> Update([FromRoute] int cod, [FromBody] ClienteUpdateDto req, CancellationToken ct)
    {
        try
        {
            var update = await _clienteService
                .UpdateAsync(cod, req, ct);
            return StatusCode(StatusCodes.Status200OK, update);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("delete/{cod}")]
    [Authorize(Policy = Permissions.Cliente.Create)]
    public async Task<IActionResult> Delete([FromRoute] int cod, CancellationToken ct)
    {
        try
        {
            var deleted = await _clienteService.DeleteAsync(cod, ct);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
