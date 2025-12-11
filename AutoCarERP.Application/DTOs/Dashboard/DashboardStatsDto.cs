namespace AutoCarERP.Application.DTOs.Dashboard;

public class DashboardStatsDto
{
    public int TotalClientes { get; set; }
    public int TotalVeiculos { get; set; }
    public int OsAberta { get; set; }
    public int OsEmAndamento { get; set; }
    public int OsAtrasada { get; set; }
    public decimal FaturamentoMes { get; set; }
}
