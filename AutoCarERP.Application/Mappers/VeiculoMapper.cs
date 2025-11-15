using AutoCarERP.Application.DTOs.Veiculo;
using AutoCarERP.Core.Entities;

namespace AutoCarERP.Application.Mappers;

public static class VeiculoMapper
{
    public static VeiculoReadDto ToReadDto(this Veiculo entity)
    {
        return new VeiculoReadDto
        {
            Codigo = entity.Codigo,
            Placa = entity.Placa,
            Marca = entity.Marca,
            Modelo = entity.Modelo,
            Cor = entity.Cor,
            Ano = entity.Ano
        };
    }
}
