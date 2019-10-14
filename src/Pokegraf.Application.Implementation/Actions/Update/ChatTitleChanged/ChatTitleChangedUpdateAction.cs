using Pokegraf.Application.Contract.Action.Update;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Actions.Update.ChatTitleChanged
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