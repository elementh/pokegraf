using System;
using System.Threading;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Core.Client;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Contract.Service;
using Pokegraf.Common.ErrorHandling;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

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
            Bot.Client.OnUpdate += HandleOnUpdate;
            Bot.Client.OnMessage += HandleOnMessage;
            Bot.Client.OnCallbackQuery += HandleOnCallbackQuery;
            Bot.Client.OnInlineQuery += HandleOnInlineQuery;

            Bot.Start();

            Thread.Sleep(int.MaxValue);
        }
        
        private async void HandleOnUpdate(object sender, UpdateEventArgs e)
        {
            try
            {
                using var scope = ServiceScopeFactory.CreateScope();
                var botContext = scope.ServiceProvider.GetRequiredService<IBotContext>();
                await botContext.Populate(e.Update);

                var mediatR = scope.ServiceProvider.GetRequiredService<IMediator>();
                var actionClient = scope.ServiceProvider.GetRequiredService<IActionClient>();

                var actionMatch = actionClient.GetUpdateAction();

                if (!actionMatch.IsError && actionMatch.Error.Type != ResultErrorType.NotFound)
                {
                    Logger.LogError("Unknown error getting update action ({@Error}).", actionMatch.Error);
                }
                else if (actionMatch.IsSuccess)
                {
                    var actionResult = await mediatR.Send(actionMatch.Value);

                    if (actionResult.IsError)
                    {
                        Logger.LogError("{UpdateAction} was not processed correctly: {@Error}",
                            actionMatch.Value.GetType().Name, actionResult.Error);
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled error processing update ({@Update}).", e.Update);
            }
        }

        private async void HandleOnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                using var scope = ServiceScopeFactory.CreateScope();
                var botContext = scope.ServiceProvider.GetRequiredService<IBotContext>();
                await botContext.Populate(e.Message);

                var mediatR = scope.ServiceProvider.GetRequiredService<IMediator>();
                var actionClient = scope.ServiceProvider.GetRequiredService<IActionClient>();

                var actionMatch = actionClient.GetCommandAction();

                if (!actionMatch.IsError && actionMatch.Error.Type != ResultErrorType.NotFound)
                {
                    Logger.LogError("Unknown error getting command action ({@Error}).", actionMatch.Error);
                }
                else if (actionMatch.IsSuccess)
                {
                    await botContext.BotClient.Client.SendChatActionAsync(e.Message.Chat.Id, ChatAction.Typing);

                    var actionResult = await mediatR.Send(actionMatch.Value);

                    if (actionResult.IsError)
                    {
                        Logger.LogError("{BotAction} was not processed correctly: {@Error}",
                            actionMatch.Value.GetType().Name, actionResult.Error);
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
                using var scope = ServiceScopeFactory.CreateScope();
                var botContext = scope.ServiceProvider.GetRequiredService<IBotContext>();
                await botContext.Populate(e.CallbackQuery);

                var mediatR = scope.ServiceProvider.GetRequiredService<IMediator>();
                var actionClient = scope.ServiceProvider.GetRequiredService<IActionClient>();

                var actionMatch = actionClient.GetCallbackAction();

                if (!actionMatch.IsError && actionMatch.Error.Type != ResultErrorType.NotFound)
                {
                    Logger.LogError("Unknown error getting callback query action ({@Error}).", actionMatch.Error);
                }
                else if (actionMatch.IsSuccess)
                {
                    var actionResult = await mediatR.Send(actionMatch.Value);

                    if (actionResult.IsError)
                    {
                        Logger.LogError("{CallbackQueryAction} was not processed correctly: {@Error}",
                            actionMatch.Value.GetType().Name, actionResult.Error);
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
                using var scope = ServiceScopeFactory.CreateScope();
                var botContext = scope.ServiceProvider.GetRequiredService<IBotContext>();
                await botContext.Populate(e.InlineQuery);

                var mediatR = scope.ServiceProvider.GetRequiredService<IMediator>();
                var actionClient = scope.ServiceProvider.GetRequiredService<IActionClient>();
                
                var actionMatch = actionClient.GetInlineAction();

                if (!actionMatch.IsError && actionMatch.Error.Type != ResultErrorType.NotFound)
                {
                    Logger.LogError("Unknown error getting inline query action ({@Error}).", actionMatch.Error);
                }
                else if (actionMatch.IsSuccess)
                {
                    var actionResult = await mediatR.Send(actionMatch.Value);

                    if (actionResult.IsError)
                    {
                        Logger.LogError("{InlineQuery} was not processed correctly: {@Error}",
                            actionMatch.Value.GetType().Name, actionResult.Error);
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