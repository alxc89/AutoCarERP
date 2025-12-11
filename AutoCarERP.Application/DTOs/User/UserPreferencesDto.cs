namespace AutoCarERP.Application.DTOs.User;

public class UserPreferencesDto
{
    public string Tema { get; set; } = "light"; // "light" or "dark"
    public string Idioma { get; set; } = "pt-BR"; // "pt-BR" or "en-US"
    public bool NotificarOsAtrasada { get; set; } = true;
}
