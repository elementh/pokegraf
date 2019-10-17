using Pokegraf.Application.Contract.Core.Action.Command;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Actions.Command.Stats
{
    public class StatsCommandAction : CommandAction
    {
        public StatsCommandAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "/stats";
        }
    }
}