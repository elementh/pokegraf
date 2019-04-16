using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.Common.Responses.Inline
{
    public class InlineResponseHandler : Pokegraf.Common.Request.RequestHandler<InlineResponse, Result>
    {
        protected readonly IBotContext BotContext;
        
        public InlineResponseHandler(ILogger<Pokegraf.Common.Request.RequestHandler<InlineResponse, Result>> logger, IMediator mediatR, IBotContext botContext) : base(logger, mediatR)
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
                Logger.LogError(e, "Unhandled error sending inline response ({@Request}).", request);
                
                return Result.UnknownError(new List<string> {e.Message});
            }
        }
    }
}