using Microsoft.AspNetCore.Mvc;
using Nyaa.Web.Services;

namespace Nyaa.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscordMessageController : ControllerBase
{
    private readonly DiscordWebhookService service;

    public DiscordMessageController(DiscordWebhookService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ActionResult> Get([FromQuery] string content)
    {
        await service.SendMessageAsync(content);

        return Ok();
    }
}
