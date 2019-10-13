using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Responses.Photo
{
    public class PhotoResponseHandler : Pokegraf.Common.Request.CommonHandler<PhotoResponse, Result>
    {
        protected readonly IBotContext BotContext;

        public PhotoResponseHandler(ILogger<Pokegraf.Common.Request.CommonHandler<PhotoResponse, Result>> logger, IMediator mediatR, IBotContext botContext) : base(logger, mediatR)
        {
            BotContext = botContext;
        }

        public override async Task<Result> Handle(PhotoResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await BotContext.BotClient.Client.SendPhotoAsync(BotContext.Chat.Id, request.Photo, cancellationToken: cancellationToken);

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