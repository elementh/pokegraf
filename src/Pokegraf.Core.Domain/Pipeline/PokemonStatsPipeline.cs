using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Navigator;
using Navigator.Abstraction;
using Pokegraf.Core.Domain.Actions.Callback.PokemonNext;
using Pokegraf.Core.Domain.Actions.Callback.PokemonPrevious;
using Pokegraf.Core.Domain.Actions.Command.Pokemon;
using Pokegraf.Core.Domain.Actions.InlineQuery.Pokemon;
using Pokegraf.Persistence.Context;

namespace Pokegraf.Core.Domain.Pipeline
{
    public class PokemonStatsPipeline :
        IRequestPostProcessor<PokemonCommandAction, Unit>,
        IRequestPostProcessor<PokemonInlineQueryAction, Unit>,
        IRequestPostProcessor<PokemonNextCallbackAction, Unit>,
        IRequestPostProcessor<PokemonPreviousCallbackAction, Unit>
    {
        protected readonly PokegrafDbContext DbContext;
        protected readonly INavigatorContext Ctx;

        public PokemonStatsPipeline(PokegrafDbContext dbContext, INavigatorContext ctx)
        {
            DbContext = dbContext;
            Ctx = ctx;
        }

        public async Task Process(PokemonCommandAction request, Unit response, CancellationToken cancellationToken)
        {
            await AddOne(Ctx.GetTelegramUser().Id);
        }

        public async Task Process(PokemonInlineQueryAction request, Unit response, CancellationToken cancellationToken)
        {
            await AddOne(Ctx.GetTelegramUser().Id);
        }

        public async Task Process(PokemonNextCallbackAction request, Unit response, CancellationToken cancellationToken)
        {
            await AddOne(Ctx.GetTelegramUser().Id);
        }

        public async Task Process(PokemonPreviousCallbackAction request, Unit response, CancellationToken cancellationToken)
        {
            await AddOne(Ctx.GetTelegramUser().Id);
        }
        
        private async Task AddOne(int trainerId)
        {
            var stats = await DbContext.Stats.FirstOrDefaultAsync(e => e.TrainerId == trainerId);

            stats.Requests.Pokemon += 1;

            await DbContext.SaveChangesAsync();
        }
    }
}