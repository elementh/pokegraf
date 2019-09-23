using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action.Update;

namespace Pokegraf.Application.Implementation.Actions.Update.ChatMemberAdded
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