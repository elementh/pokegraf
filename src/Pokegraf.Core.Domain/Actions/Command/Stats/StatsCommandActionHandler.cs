using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Abstraction;
using Navigator.Actions;

namespace Pokegraf.Core.Domain.Actions.Command.Stats
{
    public class StatsCommandActionHandler : ActionHandler<StatsCommandAction>
    {
        public StatsCommandActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override Task<Unit> Handle(StatsCommandAction request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}