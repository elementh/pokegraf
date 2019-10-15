using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Contract.Core.Responses.Text.WithKeyboard;
using Pokegraf.Common.ErrorHandling;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Application.Implementation.Core.Responses.Text.WithKeyboard
{
    public class TextWithKeyboardResponseHandler : IRequestHandler<TextWithKeyboardResponse, Status<Error>>
    {
        protected readonly ILogger<TextWithKeyboardResponseHandler> Logger;
        protected readonly IBotContext BotContext;

        public TextWithKeyboardResponseHandler(ILogger<TextWithKeyboardResponseHandler> logger, IBotContext botContext)
        {
            Logger = logger;
            BotContext = botContext;
        }

        public async Task<Status<Error>> Handle(TextWithKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await BotContext.BotClient.Client.SendTextMessageAsync(BotContext.Chat.Id, request.Text, replyMarkup: request.Keyboard, 
                    parseMode: request.ParseMode, cancellationToken: cancellationToken);

                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error sending text response with inline keyboard ({@Request}).", request);
                
                return Error(UnknownError(e.Message));
            }
        }
    }
}