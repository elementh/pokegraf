using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action;
using Pokegraf.Application.Contract.Model.Action.Callback;

namespace Pokegraf.Application.Implementation.BotActions.Callbacks.Fusion
{
    public class FusionCallbackAction : CallbackAction
    {
        public FusionCallbackAction(IBotContext botContext) : base(botContext)
        {
        }
        
        public override bool CanHandle(string condition)
        {
            return condition == "fusion";
        }
    }
}