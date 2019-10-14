using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Contract.Core.Responses.Photo.WithKeyboard.Edit;
using Pokegraf.Common.ErrorHandling;
using Telegram.Bot.Types;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Application.Implementation.Core.Responses.Photo.WithKeyboard.Edit
{
    public class PhotoWithKeyboardEditResponseHandler : IRequestHandler<PhotoWithKeyboardEditResponse, Status<Error>>
    {
        protected readonly ILogger<PhotoWithKeyboardEditResponseHandler> Logger;
        protected readonly IBotContext BotContext;
        
        public async Task<Status<Error>> Handle(PhotoWithKeyboardEditResponse request, CancellationToken cancellationToken)
        {
            try
            {
                var photo = new InputMediaPhoto(request.Photo);
                
                await BotContext.BotClient.Client.EditMessageMediaAsync(BotContext.Chat.Id, request.MessageId, photo, request.Keyboard, cancellationToken);

                await BotContext.BotClient.Client.EditMessageCaptionAsync(BotContext.Chat.Id, request.MessageId, request.Caption, request.Keyboard, cancellationToken);

                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error updating photo with caption response with inline keyboard ({@Request}).", request);
                
                return Error(UnknownError(e.Message));
            }
        }
    }
}