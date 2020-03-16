using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace Pokegraf.Core.Domain.Actions.Command.Stats
{
    public class StatsCommandAction : CommandAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return Command.ToLower() == "/stats";
        }
    }
}