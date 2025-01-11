using System.Reflection;
using Core.Framework;
using Microsoft.EntityFrameworkCore;
using Nyaa.Web.Components;
using Nyaa.Web.Database;
using Nyaa.Web.Services;

namespace Nyaa.Web;

internal static class Program
{
    public static void Main(string[] args)
    {
        var micro = new MicroApp(args);

        micro.RegisterApiDefaults();
        micro.RegisterCors();
        micro.RegisterTransient(Assembly.GetExecutingAssembly());

        micro.RegisterBuilder(
            builder =>
            {
                builder.Services.AddTransient<NyaaUnitOfWork>();
                builder.Services.AddSingleton<TokenEntityService>();
                builder.Services.AddTransient<DiscordWebhookService>();
                builder.Services.AddTransient<DiscordAuthService>();
                builder.Services.AddDbContext<NyaaDbContext>(options => { options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); });
            });

        micro.Register(
            builder =>
            {
                builder.Services
                       .AddRazorComponents()
                       .AddInteractiveServerComponents();
            },
            app =>
            {
                app.UseStaticFiles();
                app.UseAntiforgery();

                app.MapRazorComponents<App>()
                   .AddInteractiveServerRenderMode();
            });

        micro.Run();
    }
}
