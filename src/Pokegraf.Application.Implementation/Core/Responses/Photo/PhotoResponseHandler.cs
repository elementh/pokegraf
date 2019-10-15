using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Common.ErrorHandling;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Application.Implementation.Core.Responses.Photo
{
    public class PhotoResponseHandler : IRequestHandler<PhotoResponse, Status<Error>>
    {
        protected readonly ILogger<PhotoResponseHandler> Logger;
        protected readonly IBotContext BotContext;

        public PhotoResponseHandler(ILogger<PhotoResponseHandler> logger, IBotContext botContext)
        {
            Logger = logger;
            BotContext = botContext;
        }

        public async Task<Status<Error>> Handle(PhotoResponse request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Caption))
                {
                    await BotContext.BotClient.Client.SendPhotoAsync(BotContext.Chat.Id, request.Photo, cancellationToken: cancellationToken);
                }
                else
                {
                    await BotContext.BotClient.Client.SendPhotoAsync(BotContext.Chat.Id, request.Photo, request.Caption, cancellationToken: cancellationToken);
                }

                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error sending photo ({@Request}).", request);

                return Error(UnknownError(e.Message));
            }
        }
    }
}