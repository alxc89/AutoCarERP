using System.ComponentModel.DataAnnotations;

namespace AutoCarERP.Application.DTOs.Cliente;

public class ClienteCreateDto
{
    [Required, StringLength(30)]
    public string Nome { get; set; }

    [StringLength(15)]
    public string? Telefone { get; set; }

    [StringLength(15)]
    public string? CpfCnpj { get; set; }

    [StringLength(50)]
    public string? Endereco { get; set; }

    [StringLength(50), EmailAddress]
    public string? Email { get; set; }
}