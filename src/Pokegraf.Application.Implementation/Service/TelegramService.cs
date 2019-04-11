using System;
using System.Threading;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.BotActions.Common;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Application.Contract.Service;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Application.Implementation.Service
{
    public class TelegramService : ITelegramService
    {
        protected readonly ILogger<TelegramService> Logger;
        protected readonly IBotClient Bot;
        protected readonly IMediator MediatR;
        protected readonly IBotActionFactory BotActionFactory;

        public TelegramService(ILogger<TelegramService> logger, IBotClient bot, IMediator mediatR, IBotActionFactory botActionFactory)
        {
            Logger = logger;
            Bot = bot;
            MediatR = mediatR;
            BotActionFactory = botActionFactory;
        }

        public void StartPokegrafBot()
        {
            Bot.Client.OnMessage += HandleOnMessage;
            Bot.Client.OnCallbackQuery += HandleOnCallbackQuery;

            Bot.Start();

            Thread.Sleep(int.MaxValue);
        }

        private async void HandleOnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                var botActionResult = BotActionFactory.GetBotAction(e.Message);

                if (!botActionResult.Succeeded) return;

                await Bot.Client.SendChatActionAsync(e.Message.Chat.Id, ChatAction.Typing);
                
                var requestResult = await MediatR.Send(botActionResult.Value);

                if (!requestResult.Succeeded && !requestResult.Errors.ContainsKey("not_found"))
                {
                    Logger.LogError("Message request was not processed correctly.", botActionResult.Value, requestResult.Errors);
                }
            }
            catch (Exception exception)
            {
                Logger.LogError("Unhandled error processing message.", exception, e.Message);
            }
        }

        private async void HandleOnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            try
            {
                var callbackQueryResult = BotActionFactory.GetCallbackAction(e.CallbackQuery);

                if (!callbackQueryResult.Succeeded) return;

                var requestResult = await MediatR.Send(callbackQueryResult.Value);

                if (!requestResult.Succeeded && !requestResult.Errors.ContainsKey("not_found"))
                {
                    Logger.LogError("Callback query request was not processed correctly", callbackQueryResult.Value,
                        requestResult.Errors);
                }
            }
            catch (Exception exception)
            {
                Logger.LogError("Unhandled error processing callback query.", exception, e.CallbackQuery);
            }
            finally
            {
                try
                {
                    await Bot.Client.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}