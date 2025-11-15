using System;
using System.Collections.Generic;
using System.Linq;
using AutoCarERP.Core.Entities;
using AutoCarERP.Application.DTOs.Cliente;

namespace AutoCarERP.Application.Mappers
{
    /// <summary>
    /// Realiza o mapeamento de Cliente (entidade) para ClienteReadDto.
    /// </summary>
    public static class ClienteMapper
    {
        /// <summary>
        /// Converte um Cliente em ClienteReadDto.
        /// </summary>
        public static ClienteReadDto ToReadDto(this Cliente entity)
        {
            return new ClienteReadDto
            {
                Codigo   = entity.Codigo,
                Nome     = entity.Nome,
                Telefone = entity.Telefone,
                CpfCnpj  = entity.CpfCnpj,
                Endereco = entity.Endereco,
                Email    = entity.Email
            };
        }

        /// <summary>
        /// Converte uma coleção de Cliente em uma lista de ClienteReadDto.
        /// </summary>
        public static List<ClienteReadDto> ToReadDtoList(IEnumerable<Cliente> entities)
        {
            if (entities == null) return new List<ClienteReadDto>();
            return entities.Select(ToReadDto).ToList();
        }
    }
}