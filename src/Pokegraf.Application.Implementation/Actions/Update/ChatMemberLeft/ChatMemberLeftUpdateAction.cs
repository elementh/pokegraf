using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action.Update;

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