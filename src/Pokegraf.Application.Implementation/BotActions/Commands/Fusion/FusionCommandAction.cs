using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Implementation.BotActions.Common;

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