using Pokegraf.Application.Contract.Core.Action.Update;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Actions.Update.ChatMemberLeft
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