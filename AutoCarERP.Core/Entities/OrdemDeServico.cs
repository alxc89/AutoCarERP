namespace AutoCarERP.Core.Entities;

public class OrdemDeServico : Entity
{
    public DateTime HoraAbertura { get; set; }

    public DateTime? HoraFechamento { get; set; }

    // Observação: no PDF estes três campos são textos.
    // Mantidos como string para ficar 100% fiel ao documento.
    public string Veiculo { get; set; } = string.Empty;

    public string Cliente { get; set; } = string.Empty;

    public string ProdutoServico { get; set; } = string.Empty;

    public int Quantidade { get; set; }

    public decimal ValorUnitario { get; set; }

    public decimal ValorTotal { get; set; }

    public string Observacao { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}
