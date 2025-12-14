using System.ComponentModel.DataAnnotations;

namespace AutoCarERP.API.Models.Role;

public class UpdateRolePermissionsRequest
{
    [Required]
    public List<string> Permissions { get; set; } = [];
}

