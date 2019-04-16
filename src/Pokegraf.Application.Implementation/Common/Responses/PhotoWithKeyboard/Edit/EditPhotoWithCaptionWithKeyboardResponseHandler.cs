using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Common.Result;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.Common.Responses.PhotoWithKeyboard.Edit
{
    public class EditPhotoWithCaptionWithKeyboardResponseHandler : Pokegraf.Common.Request.RequestHandler<
        EditPhotoWithCaptionWithKeyboardResponse,
        Result>
    {
        protected readonly IBotClient Bot;

        public EditPhotoWithCaptionWithKeyboardResponseHandler(
            ILogger<Pokegraf.Common.Request.RequestHandler<EditPhotoWithCaptionWithKeyboardResponse, Result>> logger,
            IMediator mediatR, IBotClient bot) : base(logger, mediatR)
        {
            Bot = bot;
        }

        public override async Task<Result> Handle(EditPhotoWithCaptionWithKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await Bot.Client.EditMessageMediaAsync(request.ChatId, request.MessageId, new InputMediaPhoto(request.Photo), request.Keyboard,
                    cancellationToken: cancellationToken);

                await Bot.Client.EditMessageCaptionAsync(request.ChatId, request.MessageId, request.Caption, replyMarkup: request.Keyboard,
                    cancellationToken: cancellationToken);

                return Result.Success();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error updating photo with caption response with inline keyboard ({@Request}).", request);

                return Result.UnknownError(new List<string> {e.Message});
            }
        }
    }
}