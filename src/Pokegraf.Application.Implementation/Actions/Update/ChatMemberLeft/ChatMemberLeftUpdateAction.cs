using Pokegraf.Application.Contract.Action.Update;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Actions.Update.ChatMemberLeft
{
    public class ChatMemberLeftUpdateAction : UpdateAction
    {
        public ChatMemberLeftUpdateAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "ChatMemberLeft";
        }
    }
}