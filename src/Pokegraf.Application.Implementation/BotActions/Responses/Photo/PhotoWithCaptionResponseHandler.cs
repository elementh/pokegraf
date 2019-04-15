using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.BotActions.Responses.Photo
{
    public class PhotoWithCaptionResponseHandler : Pokegraf.Common.Request.RequestHandler<PhotoWithCaptionResponse, Result>
    {
        protected readonly IBotClient Bot;

        public PhotoWithCaptionResponseHandler(ILogger<Pokegraf.Common.Request.RequestHandler<PhotoWithCaptionResponse, Result>> logger, IMediator mediatR, IBotClient bot) : base(logger, mediatR)
        {
            Bot = bot;
        }

        public override async Task<Result> Handle(PhotoWithCaptionResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await Bot.Client.SendPhotoAsync(request.ChatId, request.Photo, request.Caption, cancellationToken: cancellationToken);

                return Result.Success();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error sending photo with caption ({@Request}).", request);
                
                return Result.UnknownError(new List<string> {e.Message});
            }
        }
    }
}