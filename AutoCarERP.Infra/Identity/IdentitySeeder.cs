using AutoCarERP.Core.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AutoCarERP.Infra.Identity;

public class IdentitySeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<IdentitySeeder> _logger;

    private static readonly string[] UserPermissions =
    [
        Permissions.Cliente.Create,
        Permissions.Cliente.Read,
        Permissions.Veiculo.Create,
        Permissions.Veiculo.Read,
        Permissions.ProdutoServico.Read,
        Permissions.OrdemDeServico.Create,
        Permissions.OrdemDeServico.Read,
        Permissions.OrdemDeServico.ItemAdd,
        Permissions.OrdemDeServico.StatusChange,
        Permissions.OrdemDeServico.PaymentUpdate,
        Permissions.OrdemDeServico.Finalize,
        Permissions.Relatorio.Generate
    ];

    private static readonly string[] ManagerPermissions =
    [
        Permissions.Cliente.Create,
        Permissions.Cliente.Read,
        Permissions.Veiculo.Create,
        Permissions.Veiculo.Read,
        Permissions.ProdutoServico.Create,
        Permissions.ProdutoServico.Read,
        Permissions.OrdemDeServico.Create,
        Permissions.OrdemDeServico.Read,
        Permissions.OrdemDeServico.ItemAdd,
        Permissions.OrdemDeServico.StatusChange,
        Permissions.OrdemDeServico.PaymentUpdate,
        Permissions.OrdemDeServico.Finalize,
        Permissions.Relatorio.Generate
    ];

    public IdentitySeeder(
        RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager,
        ILogger<IdentitySeeder> logger)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        await EnsureRoleAsync("ADMIN", Permissions.All);
        await EnsureRoleAsync("MANAGER", ManagerPermissions);
        await EnsureRoleAsync("USER", UserPermissions);

        const string adminEmail = "admin@autocarerp.local";
        const string adminPassword = "Admin@123";

        var admin = await _userManager.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(admin, adminPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Failed to create admin user: {Errors}", errors);
                return;
            }
        }

        if (!await _userManager.IsInRoleAsync(admin, "ADMIN"))
        {
            await _userManager.AddToRoleAsync(admin, "ADMIN");
        }
    }

    private async Task EnsureRoleAsync(string roleName, IEnumerable<string> permissions)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role is null)
        {
            role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Failed to create role {Role}: {Errors}", roleName, errors);
                return;
            }
        }

        var claims = await _roleManager.GetClaimsAsync(role);
        foreach (var permission in permissions)
        {
            if (claims.All(c => c.Type != Permissions.ClaimType || c.Value != permission))
            {
                await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(Permissions.ClaimType, permission));
            }
        }
    }
}
