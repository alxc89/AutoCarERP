using System.ComponentModel.DataAnnotations;

namespace AutoCarERP.API.Models.Role;

public class CreateRoleRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public List<string> Permissions { get; set; } = [];
}

