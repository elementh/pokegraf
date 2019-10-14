using System.Threading.Tasks;
using Pokegraf.Application.Contract.Core.Client;
using Telegram.Bot.Types;
using Chat = Pokegraf.Domain.Entity.Chat;
using User = Pokegraf.Domain.Entity.User;

namespace Pokegraf.Application.Contract.Core.Context
{
    public interface IBotContext
    {
        IBotClient BotClient { get; set; }
        Message Message { get; set; }
        Update Update { get; set; }
        CallbackQuery CallbackQuery { get; set; }
        InlineQuery InlineQuery { get; set; }
        User User { get; set; }
        Chat Chat { get; set; }
        string BotName { get; set; }
        
        Task Populate(Message message);
        Task Populate(CallbackQuery callbackQuery);
        Task Populate(InlineQuery inlineQuery);
        Task Populate(Update update);

    }
}