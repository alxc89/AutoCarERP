using AutoCarERP.API.Models.Auth;
using AutoCarERP.API.Services;
using AutoCarERP.Application.DTOs.User;
using AutoCarERP.Application.Services.User;
using AutoCarERP.Core.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoCarERP.API.Controllers.Auth;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IUserService userService,
    ITokenService tokenService) : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly IUserService _userService = userService;
    private readonly ITokenService _tokenService = tokenService;

    [HttpPost("register")]
    [Authorize(Policy = Permissions.Usuario.Create)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _userService.CreateUserAsync(
            new CreateUserDto(
                request.Email,
                request.Password,
                request.Role,
                request.Permissions),
            ct);

        if (!result.Success)
        {
            if (result.Error == "Usuário já existe.")
                return Conflict(new { message = result.Error });

            return BadRequest(new { message = result.Error ?? "Erro ao criar usuário." });
        }

        return StatusCode(StatusCodes.Status201Created, new { userId = result.Value });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Unauthorized("Credenciais inválidas.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        if (result.IsLockedOut)
            return StatusCode(StatusCodes.Status403Forbidden, "Sua conta está desativada. Contate o administrador.");
        if (!result.Succeeded)
            return Unauthorized("Credenciais inválidas.");

        var tokens = await _tokenService.GenerateTokensAsync(user, ct);

        return Ok(new AuthResponse
        {
            AccessToken = tokens.AccessToken,
            AccessTokenExpiresAt = tokens.AccessTokenExpiresAt,
            RefreshToken = tokens.RefreshToken,
            RefreshTokenExpiresAt = tokens.RefreshTokenExpiresAt
        });
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshAsync([FromBody] RefreshRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _tokenService.ValidateRefreshTokenAsync(request.RefreshToken, ct);
        if (user is null)
            return Unauthorized("Refresh token inválido.");

        await _tokenService.InvalidateRefreshTokenAsync(request.RefreshToken, ct);

        var tokens = await _tokenService.GenerateTokensAsync(user, ct);

        return Ok(new AuthResponse
        {
            AccessToken = tokens.AccessToken,
            AccessTokenExpiresAt = tokens.AccessTokenExpiresAt,
            RefreshToken = tokens.RefreshToken,
            RefreshTokenExpiresAt = tokens.RefreshTokenExpiresAt
        });
    }

    [HttpGet("users")]
    [Authorize(Policy = Permissions.Usuario.Read)]
    public async Task<IActionResult> ListUsersAsync(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool includeInactive = false,
        CancellationToken ct = default)
    {
        var result = await _userService.ListUsersAsync(search, page, pageSize, includeInactive, ct);
        return Ok(result);
    }

    [HttpGet("users/{id}")]
    [Authorize(Policy = Permissions.Usuario.Read)]
    public async Task<IActionResult> GetUserAsync([FromRoute] string id, CancellationToken ct)
    {
        var user = await _userService.GetUserByIdAsync(id, ct);
        return user is null ? NotFound(new { message = "Usuário não encontrado." }) : Ok(user);
    }

    [HttpPatch("users/{id}/role")]
    [Authorize(Policy = Permissions.Usuario.Edit)]
    public async Task<IActionResult> UpdateUserRoleAsync([FromRoute] string id, [FromBody] UpdateRoleRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _userService.UpdateUserRoleAsync(id, new UpdateUserRoleDto(request.Role), ct);
        if (!result.Success)
        {
            if (result.Error == "Usuário não encontrado.")
                return NotFound(new { message = result.Error });
            return BadRequest(new { message = result.Error });
        }

        return Ok(new { message = "Perfil atualizado com sucesso" });
    }

    [HttpPut("users/{id}/permissions")]
    [Authorize(Policy = Permissions.Usuario.PermissionsManage)]
    public async Task<IActionResult> UpdateUserPermissionsAsync(
        [FromRoute] string id,
        [FromBody] UpdatePermissionsRequest request,
        CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _userService.UpdateUserPermissionsAsync(id, new UpdateUserPermissionsDto(request.Permissions), ct);
        if (!result.Success)
        {
            if (result.Error == "Usuário não encontrado.")
                return NotFound(new { message = result.Error });
            return BadRequest(new { message = result.Error });
        }

        return Ok(new { message = "Permissões atualizadas com sucesso" });
    }

    [HttpPatch("users/{id}/deactivate")]
    [Authorize(Policy = Permissions.Usuario.Edit)]
    public async Task<IActionResult> DeactivateUserAsync([FromRoute] string id, CancellationToken ct)
    {
        var result = await _userService.DeactivateUserAsync(id, ct);
        if (!result.Success)
        {
            if (result.Error == "Usuário não encontrado.")
                return NotFound(new { message = result.Error });
            return BadRequest(new { message = result.Error });
        }

        return Ok(new { message = "Usuário desativado com sucesso" });
    }

    [HttpPatch("users/{id}/activate")]
    [Authorize(Policy = Permissions.Usuario.Edit)]
    public async Task<IActionResult> ActivateUserAsync([FromRoute] string id, CancellationToken ct)
    {
        var result = await _userService.ActivateUserAsync(id, ct);
        if (!result.Success)
        {
            if (result.Error == "Usuário não encontrado.")
                return NotFound(new { message = result.Error });
            return BadRequest(new { message = result.Error });
        }

        return Ok(new { message = "Usuário reativado com sucesso" });
    }

    [HttpDelete("users/{id}")]
    [Authorize(Policy = Permissions.Usuario.Delete)]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] string id, CancellationToken ct)
    {
        var result = await _userService.DeleteUserAsync(id, ct);
        if (!result.Success)
        {
            if (result.Error == "Usuário não encontrado.")
                return NotFound(new { message = result.Error });
            return BadRequest(new { message = result.Error });
        }

        return NoContent();
    }
}
