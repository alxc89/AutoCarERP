using AutoCarERP.Application.DTOs.Dashboard;

namespace AutoCarERP.Application.Services.Dashboard;

public interface IDashboardService
{
    Task<DashboardStatsDto> GetStatsAsync(CancellationToken ct = default);
    Task<List<RecentOrdemServicoDto>> GetRecentOrdensAsync(int limit = 10, CancellationToken ct = default);
}
