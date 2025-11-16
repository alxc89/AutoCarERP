using AutoCarERP.Application.DTOs.OrdemDeServico;
using AutoCarERP.Application.Services.OrdemDeServico;
using Microsoft.AspNetCore.Mvc;

namespace AutoCarERP.API.Controllers.OrdemDeServico;

[ApiController]
[Route("api/v1/[controller]")]
public class OrdemDeServicoController(IOrdemDeServicoService ordemDeServicoService) : ControllerBase
{
    private readonly IOrdemDeServicoService _ordemDeServicoService = ordemDeServicoService;

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] OrdemDeServicoCreateDto dto, CancellationToken ct)
    {
        try
        {
            var codigo = await _ordemDeServicoService.CreateAsync(dto, ct);
            return StatusCode(StatusCodes.Status201Created, codigo);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("get-by-cod/{cod}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int cod, CancellationToken ct)
    {
        try
        {
            var os = await _ordemDeServicoService.GetByIdAsync(cod, ct);
            return StatusCode(StatusCodes.Status200OK, os);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> ListAsync(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        try
        {
            var lista = await _ordemDeServicoService.ListAsync(search, page, pageSize, ct);
            return StatusCode(StatusCodes.Status200OK, lista);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("update/{cod}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int cod, [FromBody] OrdemDeServicoUpdateDto dto, CancellationToken ct)
    {
        try
        {
            var atualizado = await _ordemDeServicoService.UpdateAsync(cod, dto, ct);
            return StatusCode(StatusCodes.Status200OK, atualizado);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("delete/{cod}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int cod, CancellationToken ct)
    {
        try
        {
            await _ordemDeServicoService.DeleteAsync(cod, ct);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
