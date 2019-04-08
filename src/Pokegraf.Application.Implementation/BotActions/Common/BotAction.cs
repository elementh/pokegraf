using Pokegraf.Application.Contract.BotAction.Common;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.BotActions.Common
{
    public class BotAction : IBotAction
    {
        public int MessageId { get; set; }
        public Chat Chat { get; set; }
        public User From { get; set; }
        public string Text { get; set; }
    }
}