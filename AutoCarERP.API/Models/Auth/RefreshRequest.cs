using System.ComponentModel.DataAnnotations;

namespace AutoCarERP.API.Models.Auth;

public class RefreshRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
