using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MihaZupan.TelegramBotClients;
using Pokegraf.Application.Contract.Client;

namespace Pokegraf.Application.Implementation.Client
{
    public class BotClient : IBotClient
    {
        private ILogger<BotClient> _logger;
        public BlockingTelegramBotClient Client { get; }
        
        public BotClient(IConfiguration configuration, ILogger<BotClient> logger)
        {
            _logger = logger;
            
            try
            {
                Client = new BlockingTelegramBotClient(configuration["Telegram:Token"], (HttpClient) null/*, new SchedulerSettings(60, 10, 500, 6, 1500, 6)*/);
            }
            catch (Exception e)
            {
                _logger.LogError("Error setting TelegramBotClient", e);
                
                throw;
            }
        }
    }
}