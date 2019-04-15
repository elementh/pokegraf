using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.Common.Responses.TextWithKeyboard
{
    public class TextWithKeyboardResponseHandler : Pokegraf.Common.Request.RequestHandler<TextWithKeyboardResponse, Result>
    {
        protected readonly IBotClient Bot;

        public TextWithKeyboardResponseHandler(ILogger<Pokegraf.Common.Request.RequestHandler<TextWithKeyboardResponse, Result>> logger, IMediator mediatR, IBotClient bot) : base(logger, mediatR)
        {
            Bot = bot;
        }

        public override async Task<Result> Handle(TextWithKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await Bot.Client.SendTextMessageAsync(request.ChatId, request.Text, replyMarkup: request.Keyboard, cancellationToken: cancellationToken);

                return Result.Success();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error sending text response with inline keyboard ({@Request}).", request);
                
                return Result.UnknownError(new List<string> {e.Message});
            }
        }
    }
}