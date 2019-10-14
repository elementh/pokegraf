using Pokegraf.Application.Contract.Action.Command;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Actions.Commands.Fusion
{
    public class FusionCommandAction : CommandAction
    {
        public FusionCommandAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "/fusion";
        }
    }
}