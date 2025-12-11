namespace AutoCarERP.Application.DTOs.Dashboard;

public class RecentOrdemServicoDto
{
    public int Codigo { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime HoraAbertura { get; set; }
}
