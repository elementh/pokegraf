using Pokegraf.Application.Contract.Common.Actions;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.Common.Actions
{
    public class BotAction : Request<Result>, IBotAction
    {
        public int MessageId { get; set; }
        public Chat Chat { get; set; }
        public User From { get; set; }
        public string Text { get; set; }
    }
}