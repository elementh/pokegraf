using Pokegraf.Application.Contract.Core.Action.Command;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Actions.Commands.SetTrainerName
{
    public class SetTrainerNameCommandAction : CommandAction
    {
        public SetTrainerNameCommandAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "/setname";
        }
    }
}