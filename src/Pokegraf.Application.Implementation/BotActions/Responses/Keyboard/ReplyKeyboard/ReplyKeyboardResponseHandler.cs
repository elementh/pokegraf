using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Common.Result;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.BotActions.Responses.Keyboard.ReplyKeyboard
{
    public class ReplyKeyboardResponseHandler : Pokegraf.Common.Request.RequestHandler<ReplyKeyboardResponse, Result>
    {
        protected readonly IBotClient Bot;

        public ReplyKeyboardResponseHandler(ILogger<Pokegraf.Common.Request.RequestHandler<ReplyKeyboardResponse, Result>> logger, IMediator mediatR, IBotClient bot) : base(logger, mediatR)
        {
            Bot = bot;
        }

        public override async Task<Result> Handle(ReplyKeyboardResponse request, CancellationToken cancellationToken)
        {
            try
            {
                await Bot.Client.SendTextMessageAsync(request.ChatId, request.Text, replyMarkup: request.Keyboard, cancellationToken: cancellationToken);
                
                return Result.Success();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
        }
    }
}