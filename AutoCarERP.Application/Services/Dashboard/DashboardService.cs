using AutoCarERP.Application.DTOs.Dashboard;
using AutoCarERP.Core.Repositories;

namespace AutoCarERP.Application.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly IEfRepository<Core.Entities.Cliente> _clienteRepo;
    private readonly IEfRepository<Core.Entities.Veiculo> _veiculoRepo;
    private readonly IEfRepository<Core.Entities.OrdemDeServico> _osRepo;

    public DashboardService(
        IEfRepository<Core.Entities.Cliente> clienteRepo,
        IEfRepository<Core.Entities.Veiculo> veiculoRepo,
        IEfRepository<Core.Entities.OrdemDeServico> osRepo)
    {
        _clienteRepo = clienteRepo;
        _veiculoRepo = veiculoRepo;
        _osRepo = osRepo;
    }

    public async Task<DashboardStatsDto> GetStatsAsync(CancellationToken ct = default)
    {
        // Get all clientes (using large page size to get total count)
        var clientesResult = await _clienteRepo.GetPagedAsync(null, null, 1, int.MaxValue, false, true, ct);
        var totalClientes = clientesResult.TotalCount;

        // Get all veiculos
        var veiculosResult = await _veiculoRepo.GetPagedAsync(null, null, 1, int.MaxValue, false, true, ct);
        var totalVeiculos = veiculosResult.TotalCount;

        // Get all OS
        var osResult = await _osRepo.GetPagedAsync(null, null, 1, int.MaxValue, false, true, ct);
        var allOs = osResult.Items;
        
        var osAberta = allOs.Count(os => os.Status.Equals("ABERTA", StringComparison.OrdinalIgnoreCase));
        var osEmAndamento = allOs.Count(os => os.Status.Equals("EM_ANDAMENTO", StringComparison.OrdinalIgnoreCase) || 
                                               os.Status.Equals("EM ANDAMENTO", StringComparison.OrdinalIgnoreCase));
        
        // OS Atrasada: aquelas que ainda não têm HoraFechamento e já passaram da data prevista
        var now = DateTime.Now;
        var osAtrasada = allOs.Count(os => 
            !os.HoraFechamento.HasValue && 
            os.HoraAbertura.AddDays(7) < now && // Considera atrasada se passou 7 dias (ajuste conforme regra de negócio)
            !os.Status.Equals("FINALIZADA", StringComparison.OrdinalIgnoreCase));

        // Faturamento do mês atual (soma dos ValorTotal de OS finalizadas no mês corrente)
        var mesAtual = now.Month;
        var anoAtual = now.Year;
        var faturamentoMes = allOs
            .Where(os => 
                os.HoraFechamento.HasValue &&
                os.HoraFechamento.Value.Month == mesAtual &&
                os.HoraFechamento.Value.Year == anoAtual &&
                os.Status.Equals("FINALIZADA", StringComparison.OrdinalIgnoreCase))
            .Sum(os => os.ValorTotal);

        return new DashboardStatsDto
        {
            TotalClientes = totalClientes,
            TotalVeiculos = totalVeiculos,
            OsAberta = osAberta,
            OsEmAndamento = osEmAndamento,
            OsAtrasada = osAtrasada,
            FaturamentoMes = faturamentoMes
        };
    }

    public async Task<List<RecentOrdemServicoDto>> GetRecentOrdensAsync(int limit = 10, CancellationToken ct = default)
    {
        // Get recent OS ordered by HoraAbertura descending
        var osResult = await _osRepo.GetPagedAsync(
            null, 
            q => q.OrderByDescending(os => os.HoraAbertura), 
            1, 
            limit, 
            false, 
            true, 
            ct);
        
        var result = osResult.Items
            .Select(os => new RecentOrdemServicoDto
            {
                Codigo = os.Codigo,
                ClienteNome = $"Cliente #{os.ClienteId}", // TODO: fazer join com Cliente para pegar nome real
                Status = os.Status,
                HoraAbertura = os.HoraAbertura
            })
            .ToList();

        return result;
    }
}
