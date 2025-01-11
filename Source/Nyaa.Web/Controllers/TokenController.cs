using Microsoft.AspNetCore.Mvc;
using Nyaa.Web.Services;

namespace Nyaa.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TokenController : ControllerBase
{
    private readonly DiscordAuthService service;

    public TokenController(DiscordAuthService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ActionResult> CreateTokenAsync()
    {
        await service.SendAuthAsync();

        return Ok();
    }
}
