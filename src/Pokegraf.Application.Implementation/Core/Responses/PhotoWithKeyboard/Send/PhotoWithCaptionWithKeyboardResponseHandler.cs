using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Responses.PhotoWithKeyboard.Send
{
    public class PhotoWithCaptionWithKeyboardResponseHandler : Pokegraf.Common.Request.CommonHandler<PhotoWithCaptionWithKeyboardResponse, Result>
    {
        protected readonly IBotContext BotContext;

        public PhotoWithCaptionWithKeyboardResponseHandler(ILogger<Pokegraf.Common.Request.CommonHandler<PhotoWithCaptionWithKeyboardResponse, Result>> logger, 
            IMediator mediatR, IBotContext botContext) : base(logger, mediatR)
        {
            BotContext = botContext;
        }

        public override async Task<Result> Handle(PhotoWithCaptionWithKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await BotContext.BotClient.Client.SendPhotoAsync(BotContext.Chat.Id, request.Photo, request.Caption, replyMarkup: request.Keyboard, cancellationToken: cancellationToken);

                return Result.Success();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error sending photo with caption response with inline keyboard ({@Request}).", request);

                return Result.UnknownError(new List<string> {e.Message});
            }
        }
    }
}