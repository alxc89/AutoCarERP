using System.Linq;
using System.Linq.Expressions;
using AutoCarERP.Application.DTOs;
using AutoCarERP.Application.DTOs.Veiculo;
using AutoCarERP.Application.Mappers;
using AutoCarERP.Core.Repositories;

namespace AutoCarERP.Application.Services.Veiculo;

public class VeiculoService : IVeiculoService
{
    private readonly IEfRepository<Core.Entities.Veiculo> _efRepository;

    public VeiculoService(IEfRepository<Core.Entities.Veiculo> efRepository)
    {
        _efRepository = efRepository;
    }

    public async Task<VeiculoReadDto?> GetByPlacaAsync(string placa, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(placa))
            throw new ArgumentException("Placa obrigatória.", nameof(placa));

        placa = placa.Trim();

        var result = await _efRepository.GetPagedAsync(
            filter: v => v.Placa == placa,
            orderBy: q => q.OrderBy(v => v.Codigo),
            page: 1,
            pageSize: 1,
            ct: ct);

        var entity = result.Items.FirstOrDefault();
        if (entity is null)
            throw new Exception("Veículo não encontrado.");

        return entity.ToReadDto();
    }

    public async Task<PagedResult<VeiculoReadDto>> ListAsync(
        string? search = null,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 20;

        Expression<Func<Core.Entities.Veiculo, bool>>? predicate = null;

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            predicate = v =>
                v.Placa.Contains(search) ||
                v.Marca.Contains(search) ||
                v.Modelo.Contains(search) ||
                v.Cor.Contains(search);
        }

        var paged = await _efRepository.GetPagedAsync(
            filter: predicate,
            orderBy: q => q.OrderBy(v => v.Placa),
            page: page,
            pageSize: pageSize,
            ct: ct);

        var itens = paged.Items.Select(v => v.ToReadDto()).ToList();
        return new PagedResult<VeiculoReadDto>(itens, paged.Page, paged.PageSize, paged.TotalCount);
    }

    public async Task<int> CreateAsync(VeiculoCreateDto dto, CancellationToken ct = default)
    {
        var entity = new Core.Entities.Veiculo
        {
            Placa = dto.Placa?.Trim() ?? string.Empty,
            Marca = dto.Marca?.Trim() ?? string.Empty,
            Modelo = dto.Modelo?.Trim() ?? string.Empty,
            Cor = dto.Cor?.Trim() ?? string.Empty,
            Ano = dto.Ano
        };

        await _efRepository.AddAsync(entity, ct);
        return entity.Codigo;
    }

    public async Task<bool> UpdateAsync(int codigo, VeiculoUpdateDto dto, CancellationToken ct = default)
    {
        var entity = await _efRepository.GetByIdAsync(codigo, false, false, ct);
        if (entity is null) return false;

        entity.Placa = dto.Placa?.Trim() ?? string.Empty;
        entity.Marca = dto.Marca?.Trim() ?? string.Empty;
        entity.Modelo = dto.Modelo?.Trim() ?? string.Empty;
        entity.Cor = dto.Cor?.Trim() ?? string.Empty;
        entity.Ano = dto.Ano;

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
