using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.Common.Responses.Keyboard.ReplyKeyboard
{
    public class ReplyKeyboardResponseHandler : Pokegraf.Common.Request.RequestHandler<ReplyKeyboardResponse, Result>
    {
        protected readonly IBotContext BotContext;

        public ReplyKeyboardResponseHandler(ILogger<Pokegraf.Common.Request.RequestHandler<ReplyKeyboardResponse, Result>> logger, IMediator mediatR, IBotContext botContext) : base(logger, mediatR)
        {
            BotContext = botContext;
        }

        public override async Task<Result> Handle(ReplyKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await BotContext.BotClient.Client.SendTextMessageAsync(BotContext.Chat.Id, request.Text, replyMarkup: request.Keyboard, cancellationToken: cancellationToken);
                
                return Result.Success();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error sending reply keyboard ({@Request}).", request);
                
                return Result.UnknownError(new List<string> {e.Message});
            }

            
        }
    }
}