namespace AutoCarERP.Application.DTOs.Veiculo;

public class VeiculoReadDto
{
    public int Codigo { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string Cor { get; set; } = string.Empty;
    public int Ano { get; set; }
}
