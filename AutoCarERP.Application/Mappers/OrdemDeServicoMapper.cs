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
            ClienteId = entity.ClienteId,
            ProdutoServicoId = entity.ProdutoServicoId,
            Quantidade = entity.Quantidade,
            ValorUnitario = entity.ValorUnitario,
            ValorTotal = entity.ValorTotal,
            Observacao = entity.Observacao,
            Status = entity.Status
        };
    }
}
