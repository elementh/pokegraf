using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MihaZupan.TelegramBotClients;
using MihaZupan.TelegramBotClients.RateLimitedClient;
using Pokegraf.Application.Contract.Client;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Application.Implementation.Client
{
    public class BotClient : IBotClient
    {
        protected readonly ILogger<BotClient> Logger;
        public RateLimitedTelegramBotClient Client { get; }
        
        public bool Started { get; set; }
        
        public BotClient(IConfiguration configuration, ILogger<BotClient> logger)
        {
            Started = false;
            Logger = logger;
            
            try
            {
                Client = new RateLimitedTelegramBotClient(
                    token: configuration["POKEGRAF_TELEGRAM_TOKEN"],
                    httpClient: null,
                    schedulerSettings: new SchedulerSettings(34, 500, 1500));
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error setting TelegramBotClient.");
                
                throw;
            }
        }

        public async Task Start(CancellationToken cancellationToken = default)
        {
            if (Started == false)
            {
                var me = await Client.GetMeAsync(cancellationToken);

                Client.StartReceiving(new []
                {
                    UpdateType.Message,
                    UpdateType.CallbackQuery,
                    UpdateType.InlineQuery,
                    UpdateType.ChosenInlineResult
                }, cancellationToken);
                
                Logger.LogInformation("Telegram Bot Client is receiving updates for bot: {@BotName}", me.Username);

                var tcs = new TaskCompletionSource<bool>();
                cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
                
                await tcs.Task;
            }
            else
            {
                var me = await Client.GetMeAsync(cancellationToken);

                Logger.LogWarning("Tried to start Telegram Bot Client update receiving when it's already running for bot: {@BotName}", me.Username);
            }
        }
    }
}