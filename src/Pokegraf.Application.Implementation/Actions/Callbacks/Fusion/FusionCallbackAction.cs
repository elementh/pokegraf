using Pokegraf.Application.Contract.Action.Callback;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Actions.Callbacks.Fusion
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