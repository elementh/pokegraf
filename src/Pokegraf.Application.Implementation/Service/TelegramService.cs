using System;
using System.Threading;
using MediatR;
using Microsoft.Extensions.Logging;
using MihaZupan.TelegramBotClients;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Application.Contract.Service;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Application.Implementation.Service
{
    public class TelegramService : ITelegramService
    {
        protected readonly BlockingTelegramBotClient Bot;
        protected readonly IMediator MediatR;
        protected readonly ILogger<TelegramService> Logger;

        public TelegramService(IBotClient bot, IMediator mediatR, ILogger<TelegramService> logger)
        {
            Bot = bot.Client;
            MediatR = mediatR;
            Logger = logger;
        }

        public void StartPokegrafBot()
        {
            var me = Bot.GetMeAsync().Result;

            Bot.OnMessage += HandleOnMessage;

            Bot.StartReceiving(Array.Empty<UpdateType>());
            
            Logger.LogInformation($"Started telegram service for bot: @{me.Username}");

            Thread.Sleep(int.MaxValue);
        }

        private void HandleOnMessage(object sender, MessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}