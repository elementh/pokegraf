using Pokegraf.Application.Contract.Core.Action.Callback;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Actions.Callbacks.Start
{
    public class StartCallbackAction : CallbackAction
    {
        public StartCallbackAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "start_set_trainer_name";
        }
    }
}