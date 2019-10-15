using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Common.ErrorHandling;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Application.Implementation.Core.Responses.Text.WithKeyboard.Edit
{
    public class TextWithKeyboardEditResponseHandler : IRequestHandler<TextWithKeyboardEditResponse, Status<Error>>
    {
        protected readonly ILogger<TextWithKeyboardEditResponseHandler> Logger;
        protected readonly IBotContext BotContext;

        public TextWithKeyboardEditResponseHandler(ILogger<TextWithKeyboardEditResponseHandler> logger, IBotContext botContext)
        {
            Logger = logger;
            BotContext = botContext;
        }

        public async Task<Status<Error>> Handle(TextWithKeyboardEditResponse request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Keyboard == null)
                {
                    await BotContext.BotClient.Client.EditMessageTextAsync(BotContext.Chat.Id, request.MessageId, request.Text, 
                        request.ParseMode, cancellationToken: cancellationToken);

                }
                else
                {
                    await BotContext.BotClient.Client.EditMessageTextAsync(BotContext.Chat.Id, request.MessageId, request.Text, request.ParseMode, 
                        replyMarkup: request.Keyboard, cancellationToken: cancellationToken);
                }

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