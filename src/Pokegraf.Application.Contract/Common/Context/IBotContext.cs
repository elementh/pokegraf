using System.Threading.Tasks;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Application.Contract.Model;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Contract.Common.Context
{
    public interface IBotContext
    {
        IBotClient BotClient { get; set; }
        Message Message { get; set; }
        CallbackQuery CallbackQuery { get; set; }
        InlineQuery InlineQuery { get; set; }
        Intent Intent { get; set; }
        User User { get; set; }
        Chat Chat { get; set; }
        
        Task Populate(Message message);
        void Populate(CallbackQuery callbackQuery);
        void Populate(InlineQuery inlineQuery);
    }
}