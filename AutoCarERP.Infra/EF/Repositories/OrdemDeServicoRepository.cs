using AutoCarERP.Application.Repositories;
using AutoCarERP.Core.Entities;
using AutoCarERP.Core.Repositories;
using AutoCarERP.Core.Shared.Response;
using AutoCarERP.Infra.EF;
using Microsoft.EntityFrameworkCore;

namespace AutoCarERP.Infra.EF.Repositories;

public class OrdemDeServicoRepository : IOrdemDeServicoRepository
{
    private readonly AppDbContext _context;

    public OrdemDeServicoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrdemDeServico> GetByIdWithIncludesAsync(int codigo, CancellationToken ct = default)
    {
        return await _context.OrdensDeServico
            .Include(os => os.Cliente)
            .Include(os => os.Veiculo)
            .Include(os => os.ProdutoServico)
            .AsNoTracking()
            .FirstOrDefaultAsync(os => os.Codigo == codigo, ct);
    }

    public async Task<PagedResult<OrdemDeServico>> GetPagedWithIncludesAsync(
        string? search,
        int page,
        int pageSize,
        CancellationToken ct = default)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 20;

        IQueryable<OrdemDeServico> query = _context.OrdensDeServico
            .Include(os => os.Cliente)
            .Include(os => os.Veiculo)
            .Include(os => os.ProdutoServico)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            query = query.Where(os =>
                os.Status.Contains(search) ||
                os.Observacao.Contains(search) ||
                os.Cliente.Nome.Contains(search) ||
                os.Veiculo.Placa.Contains(search) ||
                os.ProdutoServico.Nome.Contains(search));
        }

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(os => os.HoraAbertura)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<OrdemDeServico>(items, page, pageSize, total);
    }
}
