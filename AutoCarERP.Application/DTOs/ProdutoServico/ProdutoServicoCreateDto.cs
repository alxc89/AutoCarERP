using System.ComponentModel.DataAnnotations;

namespace AutoCarERP.Application.DTOs.ProdutoServico;

public class ProdutoServicoCreateDto
{
    [Required, StringLength(30)]
    public string Nome { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Descricao { get; set; } = string.Empty;

    [StringLength(30)]
    public string? Fornecedor { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? Custo { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Valor { get; set; }
}
