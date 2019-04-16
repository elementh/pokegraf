using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.Common.Responses.Text
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
                await Bot.Client.SendTextMessageAsync(request.ChatId, request.Text, request.ParseMode, cancellationToken: cancellationToken);

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