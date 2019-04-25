using System.Collections.Generic;
using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action.Conversation;

namespace Pokegraf.Application.Implementation.BotActions.Conversation.SmallTalk
{
    public class SmallTalkConversationAction : ConversationAction
    {
        protected override string Action { get; }

        public SmallTalkConversationAction(IBotContext botContext) : base(botContext)
        {
            Action = "smalltalk";
        }

        public override bool CanHandle(string condition)
        {
            return condition.StartsWith(Action);
        }
    }
}