using AutoCarERP.Core.Entities;
using AutoCarERP.Core.Shared.Response;

namespace AutoCarERP.Application.Repositories;

/// <summary>
/// Repositório específico para OrdemDeServico com suporte a eager loading
/// </summary>
public interface IOrdemDeServicoRepository
{
    Task<OrdemDeServico?> GetByIdWithIncludesAsync(int codigo, CancellationToken ct = default);
    
    Task<PagedResult<OrdemDeServico>> GetPagedWithIncludesAsync(
        string? search,
        int page,
        int pageSize,
        CancellationToken ct = default);
}
