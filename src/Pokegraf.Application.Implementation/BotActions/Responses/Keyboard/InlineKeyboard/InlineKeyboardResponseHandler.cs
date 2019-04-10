using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.BotActions.Responses.Keyboard.InlineKeyboard
{
    public class InlineKeyboardResponseHandler : Pokegraf.Common.Request.RequestHandler<InlineKeyboardResponse, Result>
    {
        protected readonly IBotClient Bot;

        public InlineKeyboardResponseHandler(ILogger<Pokegraf.Common.Request.RequestHandler<InlineKeyboardResponse, Result>> logger, IMediator mediatR, IBotClient bot) : base(logger, mediatR)
        {
            Bot = bot;
        }

        public override async Task<Result> Handle(InlineKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await Bot.Client.SendTextMessageAsync(request.ChatId, request.Text, replyMarkup: request.Keyboard, cancellationToken: cancellationToken);

                return Result.Success();
            }
            catch (Exception e)
            {
                Logger.LogError("Unhandled error sending inline keyboard", e);
                
                return Result.UnknownError(new List<string> {e.Message});
            }
        }
    }
}