using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Responses.Inline
{
    public class InlineResponseHandler : Pokegraf.Common.Request.CommonHandler<InlineResponse, Result>
    {
        protected readonly IBotContext BotContext;
        
        public InlineResponseHandler(ILogger<Pokegraf.Common.Request.CommonHandler<InlineResponse, Result>> logger, IMediator mediatR, IBotContext botContext) : base(logger, mediatR)
        {
            BotContext = botContext;
        }

        public override async Task<Result> Handle(InlineResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await BotContext.BotClient.Client.AnswerInlineQueryAsync(BotContext.InlineQuery.Id, request.Results, cancellationToken: cancellationToken);
                
                return Result.Success();
            }
            catch (Exception e)
            {
                if (e.Message == "query is too old and response timeout expired or query ID is invalid")
                {
                    Logger.LogWarning(e,"Inline request timeout, could not answer properly ({@Request}).", request);
                    
                    return Result.Fail("timeout", new List<string> {e.Message});
                }
                
                Logger.LogError(e, "Unhandled error sending inline response ({@Request}).", request);
                
                return Result.UnknownError(new List<string> {e.Message});
            }
        }
    }
}