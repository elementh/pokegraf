using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Implementation.Common.Context;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.Common.Responses.Text
{
    public class TextResponseHandler : Pokegraf.Common.Request.CommonHandler<TextResponse, Result>
    {
        protected readonly IBotContext BotContext;

        public TextResponseHandler(ILogger<Pokegraf.Common.Request.CommonHandler<TextResponse, Result>> logger, IMediator mediatR, IBotContext botContext) : base(logger, mediatR)
        {
            BotContext = botContext;
        }

        public override async Task<Result> Handle(TextResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await BotContext.BotClient.Client.SendTextMessageAsync(BotContext.Chat.Id, request.Text, request.ParseMode, cancellationToken: cancellationToken);

                return Result.Success();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error sending text response ({@Request}).", request);
                
                return Result.UnknownError(new List<string> {e.Message});
            }
        }
    }
}