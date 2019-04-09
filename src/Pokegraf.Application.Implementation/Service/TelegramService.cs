
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.BotAction.Common;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Application.Contract.Service;
using Telegram.Bot.Args;

namespace Pokegraf.Application.Implementation.Service
{
    public class TelegramService : ITelegramService
    {
        protected readonly ILogger<TelegramService> Logger;
        protected readonly IBotClient Bot;
        protected readonly IMediator MediatR;
        protected readonly IBotActionFactory BotActionFactory;

        public TelegramService(ILogger<TelegramService> logger, IBotClient bot, IMediator mediatR, IBotActionFactory botActionFactory)
        {
            Logger = logger;
            Bot = bot;
            MediatR = mediatR;
            BotActionFactory = botActionFactory;
        }

        public void StartPokegrafBot()
        {
            var me = Bot.Client.GetMeAsync().Result;

            Bot.Client.OnMessage += HandleOnMessage;

            Bot.Start();

            Thread.Sleep(int.MaxValue);
        }
        
        private async void HandleOnMessage(object sender, MessageEventArgs e)
        {
            var result = BotActionFactory.GetBotAction(e.Message);

            if (!result.Succeeded) return;
            
            try
            {
                await MediatR.Send(result.Value);
            }
            catch (Exception exception)
            {
                Logger.LogError("Unhandled error processing message", exception, e.Message);
            }

        }
    }
}