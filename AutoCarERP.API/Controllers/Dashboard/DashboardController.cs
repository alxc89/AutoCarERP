using AutoCarERP.Application.Services.Dashboard;
using AutoCarERP.Core.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoCarERP.API.Controllers.Dashboard;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("stats")]
    [Authorize(Policy = Permissions.OrdemDeServico.Read)] // Assuming user needs OS read permission for dashboard
    public async Task<IActionResult> GetStatsAsync(CancellationToken ct)
    {
        try
        {
            var stats = await _dashboardService.GetStatsAsync(ct);
            return Ok(stats);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("recent-orders")]
    [Authorize(Policy = Permissions.OrdemDeServico.Read)]
    public async Task<IActionResult> GetRecentOrdersAsync([FromQuery] int limit = 10, CancellationToken ct = default)
    {
        try
        {
            var recentOrders = await _dashboardService.GetRecentOrdensAsync(limit, ct);
            return Ok(recentOrders);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
