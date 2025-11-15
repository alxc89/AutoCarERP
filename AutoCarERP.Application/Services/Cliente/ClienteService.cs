using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoCarERP.Application.DTOs;
using AutoCarERP.Application.DTOs.Cliente;
using AutoCarERP.Application.Mappers;
using AutoCarERP.Core.Repositories;

namespace AutoCarERP.Application.Services.Cliente
{
    public class ClienteService : IClienteService
    {
        private readonly IEfRepository<Core.Entities.Cliente> _efRepository;

        public ClienteService(IEfRepository<Core.Entities.Cliente> efRepository)
        {
            _efRepository = efRepository;
        }

        public async Task<ClienteReadDto?> GetByIdAsync(int codigo, CancellationToken ct = default)
        {
            Core.Entities.Cliente? cliente = await _efRepository
                .GetByIdAsync(codigo, false, false, ct);
            if (cliente is null)
                throw new Exception("Não foi localizado o cliente");

            return cliente!.ToReadDto();
        }

        public async Task<PagedResult<ClienteReadDto>> ListAsync(
            string? search = null,
            int page = 1,
            int pageSize = 20,
            CancellationToken ct = default)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 20;

            Expression<Func<Core.Entities.Cliente, bool>>? predicate = null;

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                predicate = c =>
                    c.Nome.Contains(search) ||
                    c.Telefone.Contains(search) ||
                    c.CpfCnpj.Contains(search) ||
                    c.Email.Contains(search);
            }

            var items = await _efRepository
                .GetPagedAsync(
                    filter: predicate,
                    orderBy: q => q.OrderBy(c => c.Nome),
                    page: page,
                    pageSize: pageSize,
                    ct: ct
                );
            if (items.TotalCount == 0)
                throw new Exception("Não foi localizado nenhum cliente");
            List<ClienteReadDto> lista = [];
            lista.AddRange(items.Items.Select(item => item.ToReadDto()));
            return new PagedResult<ClienteReadDto>(lista, page, pageSize, items.TotalCount);
        }

        public async Task<int> CreateAsync(ClienteCreateDto dto, CancellationToken ct = default)
        {
            var entity = new Core.Entities.Cliente
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone ?? string.Empty,
                CpfCnpj = dto.CpfCnpj ?? string.Empty,
                Endereco = dto.Endereco ?? string.Empty,
                Email = dto.Email ?? string.Empty,
            };

            await _efRepository.AddAsync(entity, ct);
            return entity.Codigo;
        }

        public async Task<bool> UpdateAsync(int codigo, ClienteUpdateDto dto, CancellationToken ct = default)
        {
            var entity = await _efRepository
                .GetByIdAsync(codigo, false, false, ct);
            if (entity is null) return false;

            entity.Nome = dto.Nome;
            entity.Telefone = dto.Telefone ?? string.Empty;
            entity.CpfCnpj = dto.CpfCnpj ?? string.Empty;
            entity.Endereco = dto.Endereco ?? string.Empty;
            entity.Email = dto.Email ?? string.Empty;

            await _efRepository.Update(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int codigo, CancellationToken ct = default)
        {
            var entity = await _efRepository
                .GetByIdAsync(codigo, false, false, ct);
            if (entity is null) return false;
            await _efRepository.DeleteAsync(codigo, ct);
            return true;
        }
    }
}