using AutoCarERP.Application.DTOs.Veiculo;
using AutoCarERP.Application.Services.Veiculo;
using Microsoft.AspNetCore.Mvc;

namespace AutoCarERP.API.Controllers.Veiculo;

[ApiController]
[Route("api/v1/[controller]")]
public class VeiculoController(IVeiculoService veiculoService) : ControllerBase
{
    private readonly IVeiculoService _veiculoService = veiculoService;

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] VeiculoCreateDto dto, CancellationToken ct)
    {
        try
        {
            var codigo = await _veiculoService.CreateAsync(dto, ct);
            return StatusCode(StatusCodes.Status201Created, codigo);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("get-by-placa/{placa}")]
    public async Task<IActionResult> GetByPlacaAsync([FromRoute] string placa, CancellationToken ct)
    {
        try
        {
            var veiculo = await _veiculoService.GetByPlacaAsync(placa, ct);
            return StatusCode(StatusCodes.Status200OK, veiculo);
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
            var lista = await _veiculoService.ListAsync(search, page, pageSize, ct);
            return StatusCode(StatusCodes.Status200OK, lista);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("update/{cod}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int cod, [FromBody] VeiculoUpdateDto dto, CancellationToken ct)
    {
        try
        {
            var atualizado = await _veiculoService.UpdateAsync(cod, dto, ct);
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
            await _veiculoService.DeleteAsync(cod, ct);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
