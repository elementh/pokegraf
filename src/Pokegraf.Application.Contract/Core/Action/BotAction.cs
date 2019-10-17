using System;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Domain.Entity;

namespace Pokegraf.Application.Contract.Core.Action
{
    public abstract class BotAction : IBotAction
    {
        public DateTime MessageTimestamp { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public int MessageId { get; set; }
        public Chat Chat { get; set; }
        public User From { get; set; }
        public string Text { get; set; }
        
        protected BotAction(IBotContext botContext)
        {
            MessageTimestamp = botContext.Message.Date;
            RequestTimestamp = DateTime.UtcNow;
            MessageId = botContext.Message?.MessageId ?? 0;
            Chat = botContext.Chat;
            From = botContext.User;
            Text = botContext.Message?.Text;
        }
        
        public abstract bool CanHandle(string condition);
    }
}