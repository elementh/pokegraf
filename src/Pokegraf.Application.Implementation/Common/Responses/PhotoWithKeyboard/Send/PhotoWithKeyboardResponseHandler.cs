using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.Common.Responses.PhotoWithKeyboard.Send
{
    public class PhotoWithKeyboardResponseHandler : Pokegraf.Common.Request.CommonHandler<PhotoWithKeyboardResponse, Result>
    {
        protected readonly IBotContext BotContext;

        public PhotoWithKeyboardResponseHandler(ILogger<Pokegraf.Common.Request.CommonHandler<PhotoWithKeyboardResponse, Result>> logger, 
            IMediator mediatR, IBotContext botContext) : base(logger, mediatR)
        {
            BotContext = botContext;
        }

        public override async Task<Result> Handle(PhotoWithKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await BotContext.BotClient.Client.SendPhotoAsync(BotContext.Chat.Id, request.Photo, replyMarkup: request.Keyboard,
                    cancellationToken: cancellationToken);

                return Result.Success();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error sending photo response with inline keyboard ({@Request}).", request);

                return Result.UnknownError(new List<string> {e.Message});
            }
        }
    }
}