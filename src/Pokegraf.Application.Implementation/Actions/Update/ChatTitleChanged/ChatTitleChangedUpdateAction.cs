using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action.Update;

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