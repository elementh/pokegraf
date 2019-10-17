using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Pokegraf.Common.ErrorHandling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Pokegraf.Domain.Entity.Chat;
using User = Pokegraf.Domain.Entity.User;

namespace Pokegraf.Application.Implementation.Core.Context
{
    public class BotContext : IBotContext
    {
        
        protected readonly ILogger<BotContext> Logger;
        protected readonly IMediator Mediator;
        public IBotClient BotClient { get; set; }

        public Message Message { get; set; }
        public Update Update { get; set; }
        public CallbackQuery CallbackQuery { get; set; }
        public InlineQuery InlineQuery { get; set; }
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

            if (conversationResult.IsError)
            {
                if (conversationResult.Error.Type != ErrorType.NotFound)
                {
                    var id = Guid.NewGuid();
                    Logger.LogError("Could not populate context with message. ( @Id ). @Error", id, conversationResult.Error.Message);
                    throw new Exception($"Error populating context {id}");
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
            Message = callbackQuery.Message;
            
            var conversationResult = await Mediator.Send(callbackQuery.MapToFindConversationQuery());

            if (conversationResult.IsError)
            {
                if (conversationResult.Error.Type != ErrorType.NotFound)
                {
                    var id = Guid.NewGuid();
                    Logger.LogError("Could not populate context with message. ( @Id ). @Error", id, conversationResult.Error.Message);
                    throw new Exception($"Error populating context {id}");
                }

                await Mediator.Send(callbackQuery.MapToAddConversationCommand());
                conversationResult = await Mediator.Send(callbackQuery.MapToFindConversationQuery());
            }
            
            User = conversationResult.Value.User;
            Chat = conversationResult.Value.Chat;

            var bot  = await BotClient.Client.GetMeAsync();

            BotName = bot.Username;
            

            Logger.LogTrace("Populated BotContext with CallbackQuery.", callbackQuery);
        }

        public async Task Populate(InlineQuery inlineQuery)
        {
            InlineQuery = inlineQuery;
            
            var userResult = await Mediator.Send(inlineQuery.MapToFindUserQuery());

            if (userResult.IsError)
            {
                if (userResult.Error.Type != ErrorType.NotFound)
                {
                    var id = Guid.NewGuid();
                    Logger.LogError("Could not populate context with message. ( @Id ). @Error", id, userResult.Error.Message);
                    throw new Exception($"Error populating context {id}");
                }

                await Mediator.Send(inlineQuery.MapToAddUserCommand());
                userResult = await Mediator.Send(inlineQuery.MapToFindUserQuery());
            }

            User = userResult.Value;
            
            var bot  = await BotClient.Client.GetMeAsync();

            BotName = bot.Username;
            
            Logger.LogTrace("Populated BotContext with InlineQuery.", inlineQuery);
        }
        
        public async Task Populate(Update update)
        {
            Update = update;

            if (Update.Type == UpdateType.Message)
            {
                await Populate(update.Message);
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                await Populate(update.CallbackQuery);
            }
            else if (update.Type == UpdateType.InlineQuery)
            {
                await Populate(update.InlineQuery);
            }
            
            Logger.LogTrace("Populated BotContext with Update.", update);
        }
    }
}