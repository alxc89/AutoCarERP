namespace AutoCarERP.Application.DTOs.OrdemDeServico;

public class OrdemDeServicoReadDto
{
    public int Codigo { get; set; }
    public DateTime HoraAbertura { get; set; }
    public DateTime? HoraFechamento { get; set; }
    public int VeiculoId { get; set; }
    public int ClienteId { get; set; }
    public int ProdutoServicoId { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
    public string Observacao { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
