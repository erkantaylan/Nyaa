namespace Nyaa.Web.Services;

public class DiscordWebhookService
{
    private readonly HttpClient client;
    private readonly string webhookUrl;

    public DiscordWebhookService(IConfiguration configuration, HttpClient client)
    {
        this.client = client;
        webhookUrl = configuration["Discord:AuthNotifyChannelWebhook"]!;
    }

    public async Task SendMessageAsync(string content)
    {
        var message = new DiscordWebhookMessage { Content = content };
        await client.PostAsJsonAsync(webhookUrl, message);
    }
}

public class DiscordWebhookMessage
{
    public required string Content { get; set; }
}
