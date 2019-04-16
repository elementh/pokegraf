using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Application.Contract.Common.Context;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.Common.Context
{
    public class BotContext : IBotContext
    {
        protected readonly ILogger<BotContext> Logger;
        public IBotClient BotClient { get; set; }
        public Message Message { get; set; }
        public CallbackQuery CallbackQuery { get; set; }
        public InlineQuery InlineQuery { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }

        public BotContext(ILogger<BotContext> logger, IBotClient botClient)
        {
            Logger = logger;
            BotClient = botClient;
        }

        public void Populate(Message message)
        {
            Message = message;
            User = message.From;
            Chat = message.Chat;

            Logger.LogTrace("Populated HttpContext with Message.", message);
        }

        public void Populate(CallbackQuery callbackQuery)
        {
            CallbackQuery = callbackQuery;
            Message = callbackQuery.Message;
            User = callbackQuery.From;
            Chat = callbackQuery.Message.Chat;

            Logger.LogTrace("Populated HttpContext with CallbackQuery.", callbackQuery);
        }

        public void Populate(InlineQuery inlineQuery)
        {
            InlineQuery = inlineQuery;
            User = inlineQuery.From;

            Logger.LogTrace("Populated HttpContext with InlineQuery.", inlineQuery);
        }
    }
}