using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Navigator;
using Navigator.Abstraction;
using Pokegraf.Core.Domain.Actions.Callback.MoreFusion;
using Pokegraf.Core.Domain.Actions.Command.Fusion;
using Pokegraf.Core.Domain.Actions.InlineQuery.Fusion;
using Pokegraf.Persistence.Context;

namespace Pokegraf.Core.Domain.Pipeline
{
    public class FusionStatsPipeline :
        IRequestPostProcessor<FusionCommandAction, Unit>,
        IRequestPostProcessor<MoreFusionCallbackAction, Unit>,
        IRequestPostProcessor<FusionInlineQueryAction, Unit>
    {
        protected readonly PokegrafDbContext DbContext;
        protected readonly INavigatorContext Ctx;
        public FusionStatsPipeline(PokegrafDbContext dbContext, INavigatorContext ctx)
        {
            DbContext = dbContext;
            Ctx = ctx;
        }

        public async Task Process(FusionCommandAction request, Unit response, CancellationToken cancellationToken)
        {
            await AddOne(Ctx.GetTelegramUser().Id);
        }

        public async Task Process(MoreFusionCallbackAction request, Unit response, CancellationToken cancellationToken)
        {
            await AddOne(Ctx.GetTelegramUser().Id);
        }

        public async Task Process(FusionInlineQueryAction request, Unit response, CancellationToken cancellationToken)
        {
            await AddOne(Ctx.GetTelegramUser().Id);
        }

        private async Task AddOne(int trainerId)
        {
            var stats = await DbContext.Stats.FirstOrDefaultAsync(e => e.TrainerId == trainerId);

            stats.Requests.Fusion += 1;

            await DbContext.SaveChangesAsync();
        }
    }
}