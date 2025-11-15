namespace AutoCarERP.Application.DTOs.ProdutoServico;

public class ProdutoServicoReadDto
{
    public int Codigo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Fornecedor { get; set; } = string.Empty;
    public decimal? Custo { get; set; }
    public decimal Valor { get; set; }
}
