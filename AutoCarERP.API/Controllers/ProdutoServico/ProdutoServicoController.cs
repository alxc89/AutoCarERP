using AutoCarERP.Application.DTOs.ProdutoServico;
using AutoCarERP.Application.Services.ProdutoServico;
using AutoCarERP.Core.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoCarERP.API.Controllers.ProdutoServico;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class ProdutoServicoController(IProdutoServicoService produtoServicoService) : ControllerBase
{
    private readonly IProdutoServicoService _produtoServicoService = produtoServicoService;

    [HttpPost("create")]
    [Authorize(Policy = Permissions.ProdutoServico.Create)]
    public async Task<IActionResult> CreateAsync([FromBody] ProdutoServicoCreateDto dto, CancellationToken ct)
    {
        try
        {
            var codigo = await _produtoServicoService.CreateAsync(dto, ct);
            return StatusCode(StatusCodes.Status201Created, codigo);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("get-by-cod/{cod}")]
    [Authorize(Policy = Permissions.ProdutoServico.Read)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int cod, CancellationToken ct)
    {
        try
        {
            var item = await _produtoServicoService.GetByIdAsync(cod, ct);
            return StatusCode(StatusCodes.Status200OK, item);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("get-all")]
    [Authorize(Policy = Permissions.ProdutoServico.Read)]
    public async Task<IActionResult> ListAsync(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        try
        {
            var lista = await _produtoServicoService.ListAsync(search, page, pageSize, ct);
            return StatusCode(StatusCodes.Status200OK, lista);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("update/{cod}")]
    [Authorize(Policy = Permissions.ProdutoServico.Create)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int cod, [FromBody] ProdutoServicoUpdateDto dto, CancellationToken ct)
    {
        try
        {
            var atualizado = await _produtoServicoService.UpdateAsync(cod, dto, ct);
            return StatusCode(StatusCodes.Status200OK, atualizado);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("delete/{cod}")]
    [Authorize(Policy = Permissions.ProdutoServico.Create)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int cod, CancellationToken ct)
    {
        try
        {
            await _produtoServicoService.DeleteAsync(cod, ct);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
