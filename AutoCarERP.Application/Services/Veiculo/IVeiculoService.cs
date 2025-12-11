using AutoCarERP.Application.DTOs;
using AutoCarERP.Application.DTOs.Veiculo;

namespace AutoCarERP.Application.Services.Veiculo;

public interface IVeiculoService
{
    Task<VeiculoReadDto?> GetByIdAsync(int codigo, CancellationToken ct = default);
    
    Task<VeiculoReadDto?> GetByPlacaAsync(string placa, CancellationToken ct = default);

    Task<PagedResult<VeiculoReadDto>> ListAsync(
        string? search = null,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default);

    Task<int> CreateAsync(VeiculoCreateDto dto, CancellationToken ct = default);

    Task<bool> UpdateAsync(int codigo, VeiculoUpdateDto dto, CancellationToken ct = default);

    Task<bool> DeleteAsync(int codigo, CancellationToken ct = default);
}
