using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Contract.Model.Action.Callback;

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