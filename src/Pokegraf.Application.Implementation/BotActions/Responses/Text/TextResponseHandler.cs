using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.BotActions.Responses;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.BotActions.Responses.Text
{
    public class TextResponseHandler : Pokegraf.Common.Request.RequestHandler<TextResponse, Result>
    {
        protected readonly IBotClient Bot;

        public TextResponseHandler(ILogger<Pokegraf.Common.Request.RequestHandler<TextResponse, Result>> logger, IMediator mediatR, IBotClient bot) : base(logger, mediatR)
        {
            Bot = bot;
        }

        public override async Task<Result> Handle(TextResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await Bot.Client.SendTextMessageAsync(request.ChatId, request.Text, cancellationToken: cancellationToken);

                return Result.Success();
            }
            catch (Exception e)
            {
                Logger.LogError("Unhandled error sending text response", e);
                
                return Result.UnknownError(new List<string> {e.Message});
            }
        }
    }
}