using AutoCarERP.Application.DTOs;
using AutoCarERP.Application.DTOs.ProdutoServico;

namespace AutoCarERP.Application.Services.ProdutoServico;

public interface IProdutoServicoService
{
    Task<ProdutoServicoReadDto?> GetByIdAsync(int codigo, CancellationToken ct = default);

    Task<PagedResult<ProdutoServicoReadDto>> ListAsync(
        string? search = null,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default);

    Task<int> CreateAsync(ProdutoServicoCreateDto dto, CancellationToken ct = default);

    Task<bool> UpdateAsync(int codigo, ProdutoServicoUpdateDto dto, CancellationToken ct = default);

    Task<bool> DeleteAsync(int codigo, CancellationToken ct = default);
}
