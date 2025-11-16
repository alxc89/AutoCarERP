namespace AutoCarERP.Core.Entities;

public class OrdemDeServico : Entity
{
    public DateTime HoraAbertura { get; set; }

    public DateTime? HoraFechamento { get; set; }

    public int VeiculoId { get; set; }
    public Veiculo Veiculo { get; set; } = null!;

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public int ProdutoServicoId { get; set; }
    public ProdutoServico ProdutoServico { get; set; } = null!;

    public int Quantidade { get; set; }

    public decimal ValorUnitario { get; set; }

    public decimal ValorTotal { get; set; }

    public string Observacao { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}
