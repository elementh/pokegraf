using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Core.Context;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.Core.Responses.PhotoWithKeyboard.Edit
{
    public class EditPhotoWithCaptionWithKeyboardResponseHandler : Pokegraf.Common.Request.CommonHandler<EditPhotoWithCaptionWithKeyboardResponse, Result>
    {
        protected readonly IBotContext BotContext;

        public EditPhotoWithCaptionWithKeyboardResponseHandler(ILogger<Pokegraf.Common.Request.CommonHandler<EditPhotoWithCaptionWithKeyboardResponse, Result>> logger,
            IMediator mediatR, IBotContext botContext) : base(logger, mediatR)
        {
            BotContext = botContext;
        }

        public override async Task<Result> Handle(EditPhotoWithCaptionWithKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await BotContext.BotClient.Client.EditMessageMediaAsync(BotContext.Chat.Id, request.MessageId, new InputMediaPhoto(request.Photo), request.Keyboard,
                    cancellationToken: cancellationToken);

                await BotContext.BotClient.Client.EditMessageCaptionAsync(BotContext.Chat.Id, request.MessageId, request.Caption, replyMarkup: request.Keyboard,
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