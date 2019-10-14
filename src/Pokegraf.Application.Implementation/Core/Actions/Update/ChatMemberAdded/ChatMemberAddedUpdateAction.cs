using Pokegraf.Application.Contract.Core.Action.Update;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Actions.Update.ChatMemberAdded
{
    public class ChatMemberAddedUpdateAction : UpdateAction
    {
        public ChatMemberAddedUpdateAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "ChatMembersAdded";
        }
    }
}