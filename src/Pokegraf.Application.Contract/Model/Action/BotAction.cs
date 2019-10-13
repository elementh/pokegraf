using System;
using MediatR;
using OperationResult;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Domain.Entity;

namespace Pokegraf.Application.Contract.Model.Action
{
    public abstract class BotAction : IRequest<Status>, IBotAction
    {
        public DateTime Timestamp { get; set; }
        public int MessageId { get; set; }
        public Chat Chat { get; set; }
        public User From { get; set; }
        public string Text { get; set; }
        protected BotAction(int messageId, Chat chat, User from, string text)
        {
            Timestamp = DateTime.UtcNow;
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