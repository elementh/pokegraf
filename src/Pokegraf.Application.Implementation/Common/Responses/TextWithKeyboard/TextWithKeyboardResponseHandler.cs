using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.Common.Responses.TextWithKeyboard
{
    public class TextWithKeyboardResponseHandler : Pokegraf.Common.Request.CommonHandler<TextWithKeyboardResponse, Result>
    {
        protected readonly IBotContext BotContext;

        public TextWithKeyboardResponseHandler(ILogger<Pokegraf.Common.Request.CommonHandler<TextWithKeyboardResponse, Result>> logger, 
            IMediator mediatR, IBotContext botContext) : base(logger, mediatR)
        {
            BotContext = botContext;
        }

        public override async Task<Result> Handle(TextWithKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await BotContext.BotClient.Client.SendTextMessageAsync(BotContext.Chat.Id, request.Text, replyMarkup: request.Keyboard, cancellationToken: cancellationToken);

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