using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Pokegraf.Infrastructure.Contract.Model;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Application.Implementation.Common.Context
{
    public class BotContext : IBotContext
    {
        
        protected readonly ILogger<BotContext> Logger;
        private IIntentDetectionService _intentDetectionService;
        public IBotClient BotClient { get; set; }

        public Message Message { get; set; }
        public CallbackQuery CallbackQuery { get; set; }
        public InlineQuery InlineQuery { get; set; }
        public Intent Intent { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }

        public BotContext(ILogger<BotContext> logger, IBotClient botClient, IIntentDetectionService intentDetectionService)
        {
            Logger = logger;
            BotClient = botClient;
            _intentDetectionService = intentDetectionService;
        }

        public async Task Populate(Message message)
        {
            Message = message;
            User = message.From;
            Chat = message.Chat;

            if ((Message.Entities == null || Message.Entities.All(entity => entity.Type != MessageEntityType.BotCommand)) && !string.IsNullOrWhiteSpace(Message.Text))
            {
                var intentResult = await _intentDetectionService.GetIntent(new DetectIntentQuery(Message.Text, "en-us"));

                if (intentResult.Succeeded)
                {
                    Intent = intentResult.Value.ToIntent();
                }
            }
            
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