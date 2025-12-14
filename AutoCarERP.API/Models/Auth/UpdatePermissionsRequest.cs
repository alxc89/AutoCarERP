using System.ComponentModel.DataAnnotations;

namespace AutoCarERP.API.Models.Auth;

public class UpdatePermissionsRequest
{
    [Required]
    public List<string> Permissions { get; set; } = [];
}

