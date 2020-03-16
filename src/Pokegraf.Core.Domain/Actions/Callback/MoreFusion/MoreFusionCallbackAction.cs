using Navigator.Abstraction;
using Navigator.Extensions.Actions;
using Pokegraf.Core.Domain.Resources;

namespace Pokegraf.Core.Domain.Actions.Callback.MoreFusion
{
    public class MoreFusionCallbackAction : CallbackQueryAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return Data == CallbackActions.FusionAction;
        }
    }
}