using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action;
using Pokegraf.Application.Contract.Model.Action.Command;

namespace Pokegraf.Application.Implementation.BotActions.Commands.Fusion
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