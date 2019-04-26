using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action.Conversation;

namespace Pokegraf.Application.Implementation.BotActions.Conversation.Berry.Flavor
{
    public class BerryFlavorConversationAction : ConversationAction
    {
        protected override string Action { get; }
        
        public BerryFlavorConversationAction(IBotContext botContext) : base(botContext)
        {
            Action = "berry.flavor";
        }
    }
}