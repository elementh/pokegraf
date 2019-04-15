using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.Common.Responses.PhotoWithKeyboard.Send
{
    public class PhotoWithCaptionWithKeyboardResponseHandler : Pokegraf.Common.Request.RequestHandler<PhotoWithCaptionWithKeyboardResponse,
        Result>
    {
        protected readonly IBotClient Bot;

        public PhotoWithCaptionWithKeyboardResponseHandler(ILogger<Pokegraf.Common.Request.RequestHandler<PhotoWithCaptionWithKeyboardResponse, Result>> logger,
            IMediator mediatR, IBotClient bot) : base(logger, mediatR)
        {
            Bot = bot;
        }

        public override async Task<Result> Handle(PhotoWithCaptionWithKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await Bot.Client.SendPhotoAsync(request.ChatId, request.Photo, request.Caption, replyMarkup: request.Keyboard, cancellationToken: cancellationToken);

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