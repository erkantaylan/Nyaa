using Nyaa.Web.Database;
using Nyaa.Web.Database.Entities.Token;

namespace Nyaa.Web.Services;

public class DiscordAuthService
{
    private readonly TokenEntityService entityService;
    private readonly NyaaUnitOfWork nyaa;
    private readonly DiscordWebhookService webhookService;

    public DiscordAuthService(
        DiscordWebhookService webhookService,
        TokenEntityService entityService,
        NyaaUnitOfWork nyaa
    )
    {
        this.webhookService = webhookService;
        this.entityService = entityService;
        this.nyaa = nyaa;
    }

    public async Task SendAuthAsync()
    {
        TokenEntity entity = entityService.Create();
        
        await nyaa.Token.InsertAsync(entity);
        
        DateTimeOffset toTrTime = entity.ExpiredAt.ToOffset(TimeSpan.FromHours(3));
        
        string content = $"{entity.Token}\n"
                       + $"{toTrTime}";
        
        await webhookService.SendMessageAsync(content);
    }
}
