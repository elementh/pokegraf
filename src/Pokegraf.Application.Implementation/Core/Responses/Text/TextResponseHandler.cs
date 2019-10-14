using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Contract.Core.Responses.Text;
using Pokegraf.Common.ErrorHandling;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Application.Implementation.Core.Responses.Text
{
    public class TextResponseHandler : IRequestHandler<TextResponse, Status<ResultError>>
    {
        protected readonly ILogger<TextResponseHandler> Logger;
        protected readonly IBotContext BotContext;
        
        public TextResponseHandler(ILogger<TextResponseHandler> logger, IBotContext botContext)
        {
            Logger = logger;
            BotContext = botContext;
        }

        public async Task<Status<ResultError>> Handle(TextResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await BotContext.BotClient.Client.SendTextMessageAsync(BotContext.Chat.Id, request.Text, request.ParseMode, cancellationToken: cancellationToken);

                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error sending text response ({@Request}).", request);
                
                return Error(UnknownError(e.Message));
            }
        }
    }
}