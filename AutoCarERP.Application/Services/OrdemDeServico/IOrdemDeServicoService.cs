using AutoCarERP.Application.DTOs;
using AutoCarERP.Application.DTOs.OrdemDeServico;

namespace AutoCarERP.Application.Services.OrdemDeServico;

public interface IOrdemDeServicoService
{
    Task<OrdemDeServicoReadDto?> GetByIdAsync(int codigo, CancellationToken ct = default);

    Task<PagedResult<OrdemDeServicoReadDto>> ListAsync(
        string? search = null,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default);

    Task<int> CreateAsync(OrdemDeServicoCreateDto dto, CancellationToken ct = default);

    Task<bool> UpdateAsync(int codigo, OrdemDeServicoUpdateDto dto, CancellationToken ct = default);

    Task<bool> DeleteAsync(int codigo, CancellationToken ct = default);
}
