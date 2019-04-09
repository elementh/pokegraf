
using System.Threading;
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
            
            Logger.LogInformation($"Started telegram service for bot: @{me.Username}");

            Thread.Sleep(int.MaxValue);
        }
        
        private void HandleOnMessage(object sender, MessageEventArgs e)
        {
            var botAction = BotActionFactory.GetBotAction(e.Message);

            MediatR.Send(botAction);
        }
    }
}