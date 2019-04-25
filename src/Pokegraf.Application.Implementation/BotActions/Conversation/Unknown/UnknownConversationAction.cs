using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action.Conversation;

namespace Pokegraf.Application.Implementation.BotActions.Conversation.Unknown
{
    public class UnknownConversationAction : ConversationAction    
    {
        protected override string Action { get; }

        public UnknownConversationAction(IBotContext botContext) : base(botContext)
        {
            Action = "input.unknown";
        }
    }
}