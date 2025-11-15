using AutoCarERP.Application.DTOs.ProdutoServico;
using AutoCarERP.Core.Entities;

namespace AutoCarERP.Application.Mappers;

public static class ProdutoServicoMapper
{
    public static ProdutoServicoReadDto ToReadDto(this ProdutoServico entity)
    {
        return new ProdutoServicoReadDto
        {
            Codigo = entity.Codigo,
            Nome = entity.Nome,
            Descricao = entity.Descricao,
            Fornecedor = entity.Fornecedor,
            Custo = entity.Custo,
            Valor = entity.Valor
        };
    }
}
