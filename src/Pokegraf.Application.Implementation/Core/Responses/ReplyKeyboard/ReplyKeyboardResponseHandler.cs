using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Contract.Core.Responses.ReplyKeyboard;
using Pokegraf.Application.Implementation.Core.Responses.Inline;
using Pokegraf.Common.ErrorHandling;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Application.Implementation.Core.Responses.ReplyKeyboard
{
    public class ReplyKeyboardResponseHandler : IRequestHandler<ReplyKeyboardResponse, Status<Error>>
    {
        protected readonly ILogger<InlineResponseHandler> Logger;
        protected readonly IBotContext BotContext;

        public ReplyKeyboardResponseHandler(ILogger<InlineResponseHandler> logger, IBotContext botContext)
        {
            Logger = logger;
            BotContext = botContext;
        }

        public async Task<Status<Error>> Handle(ReplyKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await BotContext.BotClient.Client.SendTextMessageAsync(BotContext.Chat.Id, request.Text, replyMarkup: request.Keyboard, cancellationToken: cancellationToken);
                
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error sending reply keyboard ({@Request}).", request);
                
                return Error(UnknownError(e.Message));
            }
        }
    }
}