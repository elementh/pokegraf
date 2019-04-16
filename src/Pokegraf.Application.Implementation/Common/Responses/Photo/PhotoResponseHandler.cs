using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.Common.Responses.Photo
{
    public class PhotoResponseHandler : Pokegraf.Common.Request.RequestHandler<PhotoResponse, Result>
    {
        protected readonly IBotClient Bot;

        public PhotoResponseHandler(ILogger<Pokegraf.Common.Request.RequestHandler<PhotoResponse, Result>> logger, IMediator mediatR, IBotClient bot) : base(logger, mediatR)
        {
            Bot = bot;
        }

        public override async Task<Result> Handle(PhotoResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await Bot.Client.SendPhotoAsync(request.ChatId, request.Photo, cancellationToken: cancellationToken);

                return Result.Success();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error sending photo ({@Request}).", request);
                
                return Result.UnknownError(new List<string> {e.Message});
            }
        }
    }
}