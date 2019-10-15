using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MihaZupan.TelegramBotClients;
using Pokegraf.Application.Contract.Core.Client;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Application.Implementation.Core.Client
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
                Client = string.IsNullOrWhiteSpace(configuration["POKEGRAF_TELEGRAM_TOKEN"]) 
                    ? new RateLimitedTelegramBotClient(configuration["Telegram:Token"]) 
                    : new RateLimitedTelegramBotClient(configuration["POKEGRAF_TELEGRAM_TOKEN"]);
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

                Client.StartReceiving(Array.Empty<UpdateType>(), cancellationToken);
                
                Logger.LogInformation($"Telegram Bot Client is receiving updates for bot: @{me.Username}");

                var tcs = new TaskCompletionSource<bool>();
                cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
                
                await tcs.Task;
            }
            else
            {
                Logger.LogWarning("Tried to start Telegram Bot Client update receiving when it's already running");
            }
        }
    }
}