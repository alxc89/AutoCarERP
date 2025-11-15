using AutoCarERP.Application.DTOs;
using AutoCarERP.Application.DTOs.Cliente;

namespace AutoCarERP.Application.Services.Cliente;

public interface IClienteService
{
    /// <summary>Obtém um cliente pelo código (ID).</summary>
    Task<ClienteReadDto?> GetByIdAsync(int codigo, CancellationToken ct = default);

    /// <summary>Lista clientes com paginação e busca opcional por nome, telefone, cpf/cnpj ou email.</summary>
    Task<PagedResult<ClienteReadDto>> ListAsync(
        string? search = null,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default);

    /// <summary>Cria um novo cliente e retorna o código gerado.</summary>
    Task<int> CreateAsync(ClienteCreateDto dto, CancellationToken ct = default);

    /// <summary>Atualiza completamente um cliente existente.</summary>
    Task<bool> UpdateAsync(int codigo, ClienteUpdateDto dto, CancellationToken ct = default);

    /// <summary>Exclui um cliente.</summary>
    Task<bool> DeleteAsync(int codigo, CancellationToken ct = default);
}