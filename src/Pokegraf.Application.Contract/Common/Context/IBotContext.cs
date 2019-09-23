using System.Threading.Tasks;
using Pokegraf.Application.Contract.Common.Client;
using Pokegraf.Application.Contract.Model;
using Telegram.Bot.Types;
using Chat = Pokegraf.Domain.Entity.Chat;
using User = Pokegraf.Domain.Entity.User;

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
        string BotName { get; set; }
        
        Task Populate(Message message);
        Task Populate(CallbackQuery callbackQuery);
        Task Populate(InlineQuery inlineQuery);
    }
}