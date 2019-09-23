using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;
using Pokegraf.Domain.Entity;

namespace Pokegraf.Application.Contract.Model.Action
{
    public abstract class BotAction : Request<Result>, IBotAction
    {
        public int MessageId { get; set; }
        public Chat Chat { get; set; }
        public User From { get; set; }
        public string Text { get; set; }
        protected BotAction(int messageId, Chat chat, User from, string text)
        {
            MessageId = messageId;
            Chat = chat;
            From = from;
            Text = text;
        }

        protected BotAction(IBotContext botContext)
        {
            MessageId = botContext.Message?.MessageId ?? 0;
            Chat = botContext.Chat;
            From = botContext.User;
            Text = botContext.Message?.Text;
        }
        
        public abstract bool CanHandle(string condition);
    }
}