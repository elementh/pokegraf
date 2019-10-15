using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Common.ErrorHandling;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Application.Implementation.Core.Responses.Photo.WithKeyboard
{
    public class PhotoWithKeyboardResponseHandler : IRequestHandler<PhotoWithKeyboardResponse, Status<Error>>
    {
        protected readonly ILogger<PhotoWithKeyboardResponseHandler> Logger;
        protected readonly IBotContext BotContext;
        
        public PhotoWithKeyboardResponseHandler(ILogger<PhotoWithKeyboardResponseHandler> logger, IBotContext botContext)
        {
            Logger = logger;
            BotContext = botContext;
        }

        public async Task<Status<Error>> Handle(PhotoWithKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Caption))
                {
                    await BotContext.BotClient.Client.SendPhotoAsync(BotContext.Chat.Id, request.Photo, replyMarkup: request.Keyboard, cancellationToken: cancellationToken);
                }
                else
                {
                    await BotContext.BotClient.Client.SendPhotoAsync(BotContext.Chat.Id, request.Photo, request.Caption, replyMarkup: request.Keyboard, cancellationToken: cancellationToken);
                }

                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error sending photo response with inline keyboard ({@Request}).", request);

                return Error(UnknownError(e.Message));
            }
        }
    }
}