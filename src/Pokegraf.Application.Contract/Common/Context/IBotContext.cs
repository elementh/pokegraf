using Pokegraf.Application.Contract.Common.Client;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Contract.Common.Context
{
    public interface IBotContext
    {
        IBotClient BotClient { get; set; }
        Message Message { get; set; }
        CallbackQuery CallbackQuery { get; set; }
        InlineQuery InlineQuery { get; set; }
        User User { get; set; }
        Chat Chat { get; set; }
        
        void Populate(Message message);
        void Populate(CallbackQuery callbackQuery);
        void Populate(InlineQuery inlineQuery);
    }
}