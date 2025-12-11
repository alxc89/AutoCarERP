using System.ComponentModel.DataAnnotations;

namespace AutoCarERP.Application.DTOs.User;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "Senha atual é obrigatória")]
    public string SenhaAtual { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nova senha é obrigatória")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 100 caracteres")]
    public string NovaSenha { get; set; } = string.Empty;
}
