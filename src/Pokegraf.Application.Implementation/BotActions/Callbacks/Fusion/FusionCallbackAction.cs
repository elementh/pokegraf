using Pokegraf.Application.Implementation.BotActions.Common;
using Pokegraf.Application.Implementation.Common.Actions;

namespace Pokegraf.Application.Implementation.BotActions.Callbacks.Fusion
{
    public class FusionCallbackAction : CallbackAction
    {
        public override bool CanHandle(string condition)
        {
            return condition == "fusion";
        }
    }
}