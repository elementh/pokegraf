using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Application.Implementation.BotActions.Responses.PhotoWithKeyboard.Send;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Dto;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.BotActions.Responses.PhotoWithKeyboard.Edit
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
                Logger.LogError("Unhandled error sending photo with caption response with inline keyboard", e);

                return Result.UnknownError(new List<string> {e.Message});
            }
        }
    }
}