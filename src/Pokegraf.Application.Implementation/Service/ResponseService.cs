using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Application.Implementation.Event;

namespace Pokegraf.Application.Implementation.Service
{
    public class ResponseService : INotificationHandler<INotification>
    {
        protected readonly ILogger<ResponseService> Logger;
        protected readonly IBotClient Bot;

        public ResponseService(ILogger<ResponseService> logger)
        {
            Logger = logger;
            Console.WriteLine("TEST");
//            Bot = bot;
        }

        public async Task Handle(INotification notification, CancellationToken cancellationToken)
        {
            Logger.LogWarning("I was HERE");
            switch (notification)
            {
//                case PhotoResponseRequest request when string.IsNullOrWhiteSpace(request.Caption):
//                    return Bot.Client.SendPhotoAsync(request.ChatId, request.Photo.ToString());
//                case PhotoResponseRequest request:
//                    return Bot.Client.SendPhotoAsync(request.ChatId, request.Photo.ToString(), request.Caption);
//                case TextResponseRequest request when !string.IsNullOrWhiteSpace(request.Text):
//                    return Bot.Client.SendTextMessageAsync(request.ChatId, request.Text);
                default:
                    return;
            }
        }

//        public Task Handle(PhotoResponseRequest notification, CancellationToken cancellationToken)
//        {
//            switch (notification)
//            {
//                case PhotoResponseRequest request when string.IsNullOrWhiteSpace(request.Caption):
//                    return Bot.Client.SendPhotoAsync(request.ChatId, request.Photo.ToString());
//                case PhotoResponseRequest request:
//                    return Bot.Client.SendPhotoAsync(request.ChatId, request.Photo.ToString(), request.Caption);
//                default:
//                    return Task.CompletedTask;
//            }
//        }
    }
}