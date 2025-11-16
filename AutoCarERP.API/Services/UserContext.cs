using System.Security.Claims;
using AutoCarERP.Application.Common.Interfaces;

namespace AutoCarERP.API.Services;

public class UserContext(IHttpContextAccessor accessor) : IUserContext
{
    private readonly IHttpContextAccessor _accessor = accessor;

    public string? UserId => _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? UserName => _accessor.HttpContext?.User.Identity?.Name;

    public IReadOnlyCollection<string> Roles =>
        _accessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray()
        ?? Array.Empty<string>();

    public IReadOnlyCollection<string> Permissions =>
        _accessor.HttpContext?.User.FindAll("perm").Select(c => c.Value).ToArray()
        ?? Array.Empty<string>();
}
