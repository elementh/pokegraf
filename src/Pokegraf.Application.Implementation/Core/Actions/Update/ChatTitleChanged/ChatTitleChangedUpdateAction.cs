using Pokegraf.Application.Contract.Core.Action.Update;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Actions.Update.ChatTitleChanged
{
    public class ChatTitleChangedUpdateAction : UpdateAction
    {
        public ChatTitleChangedUpdateAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "ChatTitleChanged";
        }
    }
}