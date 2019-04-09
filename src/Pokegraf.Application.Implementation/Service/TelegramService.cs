
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.BotActions.Common;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Application.Contract.Event;
using Pokegraf.Application.Contract.Service;
using Pokegraf.Application.Implementation.Event;
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

        public Task Handle(IResponseRequest notification, CancellationToken cancellationToken)
        {
            if (notification is PhotoResponseRequest request)
            {
                return string.IsNullOrWhiteSpace(request.Caption) 
                    ? Bot.Client.SendPhotoAsync(request.ChatId, request.Photo.ToString()) 
                    : Bot.Client.SendPhotoAsync(request.ChatId, request.Photo.ToString(), request.Caption );
            }
            
            return Task.CompletedTask;
        }
    }
}