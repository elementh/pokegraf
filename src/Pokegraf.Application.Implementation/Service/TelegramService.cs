using System;
using System.Threading;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.BotActions.Common;
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
            Bot.Client.OnMessage += HandleOnMessage;

            Bot.Start();

            Thread.Sleep(int.MaxValue);
        }
        
        private async void HandleOnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                var botActionResult = BotActionFactory.GetBotAction(e.Message);

                if (!botActionResult.Succeeded) return;
                
                var requestResult = await MediatR.Send(botActionResult.Value);

                if (!requestResult.Succeeded)
                {
                    Logger.LogWarning("Request was not processed corectly", botActionResult.Value, requestResult.Errors);
                }
            }
            catch (Exception exception)
            {
                Logger.LogError("Unhandled error processing message", exception, e.Message);
            }
        }
    }
}