using System.Linq;
using System.Linq.Expressions;
using AutoCarERP.Application.Common.Interfaces;
using AutoCarERP.Application.DTOs;
using AutoCarERP.Application.DTOs.OrdemDeServico;
using AutoCarERP.Application.Mappers;
using AutoCarERP.Application.Repositories;
using AutoCarERP.Core.Repositories;

namespace AutoCarERP.Application.Services.OrdemDeServico;

public class OrdemDeServicoService : IOrdemDeServicoService
{
    private readonly IEfRepository<Core.Entities.OrdemDeServico> _efRepository;
    private readonly IOrdemDeServicoRepository _osRepository;
    private readonly IAuditLogger _auditLogger;

    public OrdemDeServicoService(
        IEfRepository<Core.Entities.OrdemDeServico> efRepository,
        IOrdemDeServicoRepository osRepository,
        IAuditLogger auditLogger)
    {
        _efRepository = efRepository;
        _osRepository = osRepository;
        _auditLogger = auditLogger;
    }

    public async Task<OrdemDeServicoReadDto?> GetByIdAsync(int codigo, CancellationToken ct = default)
    {
        var entity = await _osRepository.GetByIdWithIncludesAsync(codigo, ct);
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
        var paged = await _osRepository.GetPagedWithIncludesAsync(search, page, pageSize, ct);
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
        await _auditLogger.LogAsync("OS.Create", nameof(Core.Entities.OrdemDeServico), entity.Codigo.ToString(), entity, ct);
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
        await _auditLogger.LogAsync("OS.Update", nameof(Core.Entities.OrdemDeServico), entity.Codigo.ToString(), entity, ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int codigo, CancellationToken ct = default)
    {
        var entity = await _efRepository.GetByIdAsync(codigo, false, false, ct);
        if (entity is null) return false;

        await _efRepository.DeleteAsync(codigo, ct);
        await _auditLogger.LogAsync("OS.Delete", nameof(Core.Entities.OrdemDeServico), codigo.ToString(), null, ct);
        return true;
    }
}
