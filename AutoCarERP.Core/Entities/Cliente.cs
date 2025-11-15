namespace AutoCarERP.Core.Entities;

public class Cliente : Entity
{
    public string Nome { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string CpfCnpj { get; set; } = string.Empty;

    public string Endereco { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
