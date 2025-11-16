using System.ComponentModel.DataAnnotations;

namespace AutoCarERP.Application.DTOs.OrdemDeServico;

public class OrdemDeServicoUpdateDto
{
    [Required]
    public DateTime HoraAbertura { get; set; }

    public DateTime? HoraFechamento { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int VeiculoId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int ClienteId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int ProdutoServicoId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantidade { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal ValorUnitario { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal ValorTotal { get; set; }

    [StringLength(50)]
    public string? Observacao { get; set; }

    [Required, StringLength(15)]
    public string Status { get; set; } = string.Empty;
}
