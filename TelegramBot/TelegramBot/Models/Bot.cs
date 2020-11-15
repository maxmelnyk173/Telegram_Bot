using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace TelegramBot.Models
{
    public static class Bot
    {
        public static IServiceCollection AddTelegramBotClient(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var client = new TelegramBotClient(configuration["Token"]);

            var webHook = $"{configuration["Url"]}/api/message/update";

            client.SetWebhookAsync(webHook).Wait();
            
            return serviceCollection.AddTransient<ITelegramBotClient>(x=> client);
        }
    }
}