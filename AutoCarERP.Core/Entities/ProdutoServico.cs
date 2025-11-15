namespace AutoCarERP.Core.Entities;

public class ProdutoServico : Entity
{
    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string Fornecedor { get; set; } = string.Empty;

    public decimal? Custo { get; set; }

    public decimal Valor { get; set; }
}
