using AutoCarERP.Core.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoCarERP.API.Controllers.Auth;

[ApiController]
[Route("api/v1/Auth/permissions")]
[Authorize]
public class PermissionsController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = Permissions.Perfil.Read)]
    public IActionResult List()
    {
        return Ok(Permissions.All.Distinct().OrderBy(p => p).ToArray());
    }
}

