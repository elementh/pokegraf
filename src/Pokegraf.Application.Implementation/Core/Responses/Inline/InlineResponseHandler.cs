using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Contract.Core.Responses.Inline;
using Pokegraf.Common.ErrorHandling;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Application.Implementation.Core.Responses.Inline
{
    public class InlineResponseHandler : IRequestHandler<InlineResponse, Status<Error>>
    {
        protected readonly ILogger<InlineResponseHandler> Logger;
        protected readonly IBotContext BotContext;


        public async Task<Status<Error>> Handle(InlineResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await BotContext.BotClient.Client.AnswerInlineQueryAsync(BotContext.InlineQuery.Id, request.Results, cancellationToken: cancellationToken);

                return Ok();
            }
            catch (Exception e)
            {
                if (e.Message == "query is too old and response timeout expired or query ID is invalid")
                {
                    Logger.LogWarning(e,"Inline request timeout, could not answer properly ({@Request}).", request);

                    return Error(Timeout(e.Message));
                }
                
                Logger.LogError(e, "Unhandled error sending inline response ({@Request}).", request);

                return Error(UnknownError(e.Message));
            }
        }
    }
}