using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Common.Strategy;
using Pokegraf.Application.Contract.Service;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

namespace Pokegraf.Application.Implementation.Service
{
    public class TelegramService : ITelegramService
    {
        protected readonly ILogger<TelegramService> Logger;
        protected readonly IBotClient Bot;
        protected readonly IServiceScopeFactory ServiceScopeFactory;

        public TelegramService(ILogger<TelegramService> logger, IBotClient bot, IServiceScopeFactory serviceScopeFactory)
        {
            Logger = logger;
            Bot = bot;
            ServiceScopeFactory = serviceScopeFactory;
        }

        public void StartPokegrafBot()
        {
            Bot.Client.OnMessage += HandleOnMessage;
            Bot.Client.OnCallbackQuery += HandleOnCallbackQuery;
            Bot.Client.OnInlineQuery += HandleOnInlineQuery;

            Bot.Start();

            Thread.Sleep(int.MaxValue);
        }

        private async void HandleOnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    var botContext = scope.ServiceProvider.GetRequiredService<IBotContext>();
                    await botContext.Populate(e.Message);

                    var mediatR = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var actionSelector = scope.ServiceProvider.GetRequiredService<IBotActionSelector>();

                    var actionResult = actionSelector.GetCommandAction();
                    
                    if (!actionResult.Succeeded && !actionResult.Errors.ContainsKey("not_found"))
                    {
                        Logger.LogError("{BotAction} was not processed correctly: {@Errors}",
                            actionResult.Value.GetType().Name, actionResult.Errors);

                        return;
                    }
                    
                    await botContext.BotClient.Client.SendChatActionAsync(e.Message.Chat.Id, ChatAction.Typing);
                    
                    var requestResult = await mediatR.Send(actionResult.Value);

                    if (!requestResult.Succeeded && !requestResult.Errors.ContainsKey("not_found"))
                    {
                        Logger.LogError("{BotAction} was not processed correctly: {@Errors}",
                            actionResult.Value.GetType().Name, requestResult.Errors);
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled error processing message ({@Message}).", e.Message);
            }
        }

        private async void HandleOnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            try
            {
                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    var botContext = scope.ServiceProvider.GetRequiredService<IBotContext>();
                    await botContext.Populate(e.CallbackQuery);

                    var mediatR = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var actionSelector = scope.ServiceProvider.GetRequiredService<IBotActionSelector>();

                    var callbackActionResult = actionSelector.GetCallbackAction();

                    if (!callbackActionResult.Succeeded) return;

                    var requestResult = await mediatR.Send(callbackActionResult.Value);

                    if (!requestResult.Succeeded && !requestResult.Errors.ContainsKey("not_found"))
                    {
                        Logger.LogError("{CallbackQueryAction} was not processed correctly: {@Errors}",
                            callbackActionResult.Value.GetType().Name, requestResult.Errors);
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled error processing callback query ({@CallbackQuery}).", e.CallbackQuery);
            }
            finally
            {
#pragma warning disable 4014
                Bot.Client.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
#pragma warning restore 4014
            }
        }

        private async void HandleOnInlineQuery(object sender, InlineQueryEventArgs e)
        {
            try
            {
                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    var botContext = scope.ServiceProvider.GetRequiredService<IBotContext>();
                    await botContext.Populate(e.InlineQuery);

                    var mediatR = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var actionSelector = scope.ServiceProvider.GetRequiredService<IBotActionSelector>();


                    var inlineActionResult = actionSelector.GetInlineAction();

                    if (!inlineActionResult.Succeeded) return;

                    var requestResult = await mediatR.Send(inlineActionResult.Value);

                    if (!requestResult.Succeeded && !requestResult.Errors.ContainsKey("not_found"))
                    {
                        Logger.LogError("{InlineQueryAction} request was not processed correctly: {@Errors}",
                            inlineActionResult.Value.GetType().Name, requestResult.Errors);
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled error processing inline query ({@InlineQuery}).", e.InlineQuery);
            }
        }
    }
}