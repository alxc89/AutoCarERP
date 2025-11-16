using System.Linq;
using System.Linq.Expressions;
using AutoCarERP.Application.Common.Interfaces;
using AutoCarERP.Application.DTOs;
using AutoCarERP.Application.DTOs.ProdutoServico;
using AutoCarERP.Application.Mappers;
using AutoCarERP.Core.Repositories;

namespace AutoCarERP.Application.Services.ProdutoServico;

public class ProdutoServicoService : IProdutoServicoService
{
    private readonly IEfRepository<Core.Entities.ProdutoServico> _efRepository;
    private readonly IAuditLogger _auditLogger;

    public ProdutoServicoService(IEfRepository<Core.Entities.ProdutoServico> efRepository, IAuditLogger auditLogger)
    {
        _efRepository = efRepository;
        _auditLogger = auditLogger;
    }

    public async Task<ProdutoServicoReadDto?> GetByIdAsync(int codigo, CancellationToken ct = default)
    {
        var entity = await _efRepository.GetByIdAsync(codigo, false, false, ct);
        if (entity is null)
            throw new Exception("Produto/Serviço não encontrado.");

        return entity.ToReadDto();
    }

    public async Task<PagedResult<ProdutoServicoReadDto>> ListAsync(
        string? search = null,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 20;

        Expression<Func<Core.Entities.ProdutoServico, bool>>? predicate = null;

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            predicate = p =>
                p.Nome.Contains(search) ||
                p.Descricao.Contains(search) ||
                (p.Fornecedor ?? string.Empty).Contains(search);
        }

        var paged = await _efRepository.GetPagedAsync(
            filter: predicate,
            orderBy: q => q.OrderBy(p => p.Nome),
            page: page,
            pageSize: pageSize,
            ct: ct);

        var itens = paged.Items.Select(p => p.ToReadDto()).ToList();
        return new PagedResult<ProdutoServicoReadDto>(itens, paged.Page, paged.PageSize, paged.TotalCount);
    }

    public async Task<int> CreateAsync(ProdutoServicoCreateDto dto, CancellationToken ct = default)
    {
        var entity = new Core.Entities.ProdutoServico
        {
            Nome = dto.Nome?.Trim() ?? string.Empty,
            Descricao = dto.Descricao?.Trim() ?? string.Empty,
            Fornecedor = dto.Fornecedor?.Trim() ?? string.Empty,
            Custo = dto.Custo,
            Valor = dto.Valor
        };

        await _efRepository.AddAsync(entity, ct);
        await _auditLogger.LogAsync("ProdutoServico.Create", nameof(Core.Entities.ProdutoServico), entity.Codigo.ToString(), entity, ct);
        return entity.Codigo;
    }

    public async Task<bool> UpdateAsync(int codigo, ProdutoServicoUpdateDto dto, CancellationToken ct = default)
    {
        var entity = await _efRepository.GetByIdAsync(codigo, false, false, ct);
        if (entity is null) return false;

        entity.Nome = dto.Nome?.Trim() ?? string.Empty;
        entity.Descricao = dto.Descricao?.Trim() ?? string.Empty;
        entity.Fornecedor = dto.Fornecedor?.Trim() ?? string.Empty;
        entity.Custo = dto.Custo;
        entity.Valor = dto.Valor;

        await _efRepository.Update(entity);
        await _auditLogger.LogAsync("ProdutoServico.Update", nameof(Core.Entities.ProdutoServico), entity.Codigo.ToString(), entity, ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int codigo, CancellationToken ct = default)
    {
        var entity = await _efRepository.GetByIdAsync(codigo, false, false, ct);
        if (entity is null) return false;

        await _efRepository.DeleteAsync(codigo, ct);
        await _auditLogger.LogAsync("ProdutoServico.Delete", nameof(Core.Entities.ProdutoServico), codigo.ToString(), null, ct);
        return true;
    }
}
