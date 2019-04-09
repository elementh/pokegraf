using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MihaZupan.TelegramBotClients;
using Pokegraf.Application.Contract.Client;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Application.Implementation.Client
{
    public class BotClient : IBotClient
    {
        protected readonly ILogger<BotClient> Logger;
        public BlockingTelegramBotClient Client { get; }
        
        public bool Started { get; set; }
        
        public BotClient(IConfiguration configuration, ILogger<BotClient> logger)
        {
            Started = false;
            Logger = logger;
            
            try
            {
                Client = new BlockingTelegramBotClient(configuration["Telegram:Token"], (HttpClient) null/*, new SchedulerSettings(60, 10, 500, 6, 1500, 6)*/);
            }
            catch (Exception e)
            {
                Logger.LogError("Error setting TelegramBotClient", e);
                
                throw;
            }
        }

        public void Start()
        {
            if (Started == false)
            {
                Client.StartReceiving(Array.Empty<UpdateType>());
            }
        }
    }
}