using System.Linq;
using System.Linq.Expressions;
using AutoCarERP.Application.DTOs;
using AutoCarERP.Application.DTOs.OrdemDeServico;
using AutoCarERP.Application.Mappers;
using AutoCarERP.Core.Repositories;

namespace AutoCarERP.Application.Services.OrdemDeServico;

public class OrdemDeServicoService : IOrdemDeServicoService
{
    private readonly IEfRepository<Core.Entities.OrdemDeServico> _efRepository;

    public OrdemDeServicoService(IEfRepository<Core.Entities.OrdemDeServico> efRepository)
    {
        _efRepository = efRepository;
    }

    public async Task<OrdemDeServicoReadDto?> GetByIdAsync(int codigo, CancellationToken ct = default)
    {
        var entity = await _efRepository.GetByIdAsync(codigo, false, false, ct);
        if (entity is null)
            throw new Exception("Ordem de Serviço não encontrada.");

        return entity.ToReadDto();
    }

    public async Task<PagedResult<OrdemDeServicoReadDto>> ListAsync(
        string? search = null,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 20;

        Expression<Func<Core.Entities.OrdemDeServico, bool>>? predicate = null;

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            predicate = os =>
                os.Status.Contains(search) ||
                os.Observacao.Contains(search);
        }

        var paged = await _efRepository.GetPagedAsync(
            filter: predicate,
            orderBy: q => q.OrderByDescending(os => os.HoraAbertura),
            page: page,
            pageSize: pageSize,
            ct: ct);

        var itens = paged.Items.Select(os => os.ToReadDto()).ToList();
        return new PagedResult<OrdemDeServicoReadDto>(itens, paged.Page, paged.PageSize, paged.TotalCount);
    }

    public async Task<int> CreateAsync(OrdemDeServicoCreateDto dto, CancellationToken ct = default)
    {
        var entity = new Core.Entities.OrdemDeServico
        {
            HoraAbertura = dto.HoraAbertura,
            HoraFechamento = dto.HoraFechamento,
            VeiculoId = dto.VeiculoId,
            ClienteId = dto.ClienteId,
            ProdutoServicoId = dto.ProdutoServicoId,
            Quantidade = dto.Quantidade,
            ValorUnitario = dto.ValorUnitario,
            ValorTotal = dto.ValorTotal,
            Observacao = dto.Observacao?.Trim() ?? string.Empty,
            Status = dto.Status.Trim()
        };

        await _efRepository.AddAsync(entity, ct);
        return entity.Codigo;
    }

    public async Task<bool> UpdateAsync(int codigo, OrdemDeServicoUpdateDto dto, CancellationToken ct = default)
    {
        var entity = await _efRepository.GetByIdAsync(codigo, false, false, ct);
        if (entity is null) return false;

        entity.HoraAbertura = dto.HoraAbertura;
        entity.HoraFechamento = dto.HoraFechamento;
        entity.VeiculoId = dto.VeiculoId;
        entity.ClienteId = dto.ClienteId;
        entity.ProdutoServicoId = dto.ProdutoServicoId;
        entity.Quantidade = dto.Quantidade;
        entity.ValorUnitario = dto.ValorUnitario;
        entity.ValorTotal = dto.ValorTotal;
        entity.Observacao = dto.Observacao?.Trim() ?? string.Empty;
        entity.Status = dto.Status.Trim();

        await _efRepository.Update(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(int codigo, CancellationToken ct = default)
    {
        var entity = await _efRepository.GetByIdAsync(codigo, false, false, ct);
        if (entity is null) return false;

        await _efRepository.DeleteAsync(codigo, ct);
        return true;
    }
}
