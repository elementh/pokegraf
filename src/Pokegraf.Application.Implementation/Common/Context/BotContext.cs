using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Telegram.Bot.Types;
using Chat = Pokegraf.Domain.Entity.Chat;
using User = Pokegraf.Domain.Entity.User;

namespace Pokegraf.Application.Implementation.Common.Context
{
    public class BotContext : IBotContext
    {
        
        protected readonly ILogger<BotContext> Logger;
        protected readonly IMediator Mediator;
        public IBotClient BotClient { get; set; }

        public Message Message { get; set; }
        public CallbackQuery CallbackQuery { get; set; }
        public InlineQuery InlineQuery { get; set; }
        public Intent Intent { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }
        public string BotName { get; set; }

        public BotContext(ILogger<BotContext> logger, IMediator mediator, IBotClient botClient)
        {
            Logger = logger;
            Mediator = mediator;
            BotClient = botClient;
        }

        public async Task Populate(Message message)
        {
            Message = message;
            
            var conversationResult = await Mediator.Send(message.MapToFindConversationQuery());

            if (!conversationResult.Succeeded)
            {
                if (!conversationResult.Errors.ContainsKey("not_found"))
                {
                    Logger.LogError("Could not populate context with message. @Errors", conversationResult.Errors);
                    throw new Exception("Error populating context");
                }

                await Mediator.Send(message.MapToAddConversationCommand());
                conversationResult = await Mediator.Send(message.MapToFindConversationQuery());
            }
            
            User = conversationResult.Value.User;
            Chat = conversationResult.Value.Chat;

            var bot  = await BotClient.Client.GetMeAsync();

            BotName = bot.Username;
            
            Logger.LogTrace("Populated BotContext with Message.", message);
        }

        public async Task Populate(CallbackQuery callbackQuery)
        {
            CallbackQuery = callbackQuery;

            await Populate(callbackQuery.Message);

            Logger.LogTrace("Populated HttpContext with CallbackQuery.", callbackQuery);
        }

        public async Task Populate(InlineQuery inlineQuery)
        {
            InlineQuery = inlineQuery;
            
            var userResult = await Mediator.Send(inlineQuery.MapToFindUserQuery());

            if (!userResult.Succeeded)
            {
                if (!userResult.Errors.ContainsKey("not_found"))
                {
                    Logger.LogError("Could not populate context with InlineQuery. @Errors", userResult.Errors);
                    throw new Exception("Error populating context");
                }

                await Mediator.Send(inlineQuery.MapToAddUserCommand());
                userResult = await Mediator.Send(inlineQuery.MapToFindUserQuery());
            }

            User = userResult.Value;
            
            var bot  = await BotClient.Client.GetMeAsync();

            BotName = bot.Username;
            
            Logger.LogTrace("Populated HttpContext with InlineQuery.", inlineQuery);
        }
    }
}