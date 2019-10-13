using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Responses.Keyboard.ReplyKeyboard
{
    public class ReplyKeyboardResponseHandler : Pokegraf.Common.Request.CommonHandler<ReplyKeyboardResponse, Result>
    {
        protected readonly IBotContext BotContext;

        public ReplyKeyboardResponseHandler(ILogger<Pokegraf.Common.Request.CommonHandler<ReplyKeyboardResponse, Result>> logger, IMediator mediatR, IBotContext botContext) : base(logger, mediatR)
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