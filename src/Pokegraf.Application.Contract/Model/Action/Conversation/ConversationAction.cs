using System.Collections.Generic;
using Pokegraf.Application.Contract.Common.Context;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Contract.Model.Action.Conversation
{
    public abstract class ConversationAction : BotAction, IConversationAction
    {
        protected abstract string Action { get; }
        protected abstract Dictionary<string, string> Parameters { get; set; }

        protected ConversationAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == Action;
        }
    }
}