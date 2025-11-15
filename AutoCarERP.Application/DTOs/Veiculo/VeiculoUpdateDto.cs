using System.ComponentModel.DataAnnotations;

namespace AutoCarERP.Application.DTOs.Veiculo;

public class VeiculoUpdateDto
{
    [Required, StringLength(10)]
    public string Placa { get; set; } = string.Empty;

    [Required, StringLength(15)]
    public string Marca { get; set; } = string.Empty;

    [Required, StringLength(15)]
    public string Modelo { get; set; } = string.Empty;

    [Required, StringLength(15)]
    public string Cor { get; set; } = string.Empty;

    [Required]
    public int Ano { get; set; }
}
