using AutoCarERP.Application.DTOs.OrdemDeServico;
using AutoCarERP.Core.Entities;

namespace AutoCarERP.Application.Mappers;

public static class OrdemDeServicoMapper
{
    public static OrdemDeServicoReadDto ToReadDto(this OrdemDeServico entity)
    {
        return new OrdemDeServicoReadDto
        {
            Codigo = entity.Codigo,
            HoraAbertura = entity.HoraAbertura,
            HoraFechamento = entity.HoraFechamento,
            
            VeiculoId = entity.VeiculoId,
            VeiculoPlaca = entity.Veiculo?.Placa ?? string.Empty,
            VeiculoModelo = entity.Veiculo != null ? $"{entity.Veiculo.Marca} {entity.Veiculo.Modelo}" : string.Empty,
            
            ClienteId = entity.ClienteId,
            ClienteNome = entity.Cliente?.Nome ?? string.Empty,
            
            ProdutoServicoId = entity.ProdutoServicoId,
            ProdutoServicoNome = entity.ProdutoServico?.Nome ?? string.Empty,
            
            Quantidade = entity.Quantidade,
            ValorUnitario = entity.ValorUnitario,
            ValorTotal = entity.ValorTotal,
            Observacao = entity.Observacao,
            Status = entity.Status
        };
    }
}
