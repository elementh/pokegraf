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
    public class PhotoWithKeyboardResponseHandler : Pokegraf.Common.Request.RequestHandler<PhotoWithKeyboardResponse, Result>
    {
        protected readonly IBotClient Bot;

        public PhotoWithKeyboardResponseHandler(ILogger<Pokegraf.Common.Request.RequestHandler<PhotoWithKeyboardResponse, Result>> logger,
            IMediator mediatR, IBotClient bot) : base(logger, mediatR)
        {
            Bot = bot;
        }

        public override async Task<Result> Handle(PhotoWithKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await Bot.Client.SendPhotoAsync(request.ChatId, request.Photo, replyMarkup: request.Keyboard,
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