namespace AutoCarERP.Application.DTOs.OrdemDeServico;

public class OrdemDeServicoReadDto
{
    public int Codigo { get; set; }
    public DateTime HoraAbertura { get; set; }
    public DateTime? HoraFechamento { get; set; }
    
    public int VeiculoId { get; set; }
    public string VeiculoPlaca { get; set; } = string.Empty;
    public string VeiculoModelo { get; set; } = string.Empty;
    
    public int ClienteId { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
    
    public int ProdutoServicoId { get; set; }
    public string ProdutoServicoNome { get; set; } = string.Empty;
    
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
    public string Observacao { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
