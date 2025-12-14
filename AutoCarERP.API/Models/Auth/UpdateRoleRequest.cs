using System.ComponentModel.DataAnnotations;

namespace AutoCarERP.API.Models.Auth;

public class UpdateRoleRequest
{
    [Required]
    public string Role { get; set; } = string.Empty;
}

