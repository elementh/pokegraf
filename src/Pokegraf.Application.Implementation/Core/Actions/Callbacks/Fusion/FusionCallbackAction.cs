using Pokegraf.Application.Contract.Core.Action.Callback;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Actions.Callbacks.Fusion
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