using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Pokegraf.Common.Resources;
using Pokegraf.Core.Domain.Stats.Service;
using Pokegraf.Persistence.Context;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Core.Domain.Actions.Command.Stats
{
    public class StatsCommandActionHandler : ActionHandler<StatsCommandAction>
    {
        protected readonly IGlobalStatsService GlobalStatsService;
        protected readonly PokegrafDbContext DbContext;
        
        public StatsCommandActionHandler(INavigatorContext ctx, IGlobalStatsService globalStatsService, PokegrafDbContext dbContext) : base(ctx)
        {
            GlobalStatsService = globalStatsService;
            DbContext = dbContext;
        }

        public override async Task<Unit> Handle(StatsCommandAction request, CancellationToken cancellationToken)
        {
            var globalStats = await GlobalStatsService.Get(cancellationToken);

            if (globalStats == null)
            {
                await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(),
                    PokegrafResources.StatsErrorMessage,
                    ParseMode.Markdown,
                    cancellationToken: cancellationToken);
                
                return default;
            }
            
            var messageBuilder = new StringBuilder();
            messageBuilder.Append($"There are a total of *{globalStats.Users} trainers* and *{globalStats.Chats} parties* in [@pokegraf_bot](https://t.me/pokegraf_bot) !");
            messageBuilder.Append($"\n\nTrainers have requested a total of *{globalStats.PokemonRequests} pokémon* and *{globalStats.FusionRequests} fusions*!");

            var userStats = await DbContext.Stats.FirstOrDefaultAsync(e => e.TrainerId == Ctx.GetTelegramUser().Id, cancellationToken);

            if (userStats != null)
            {
                messageBuilder.Append($"\n\nYou have requested *{userStats.Requests.Pokemon} pokemons* and *{userStats.Requests.Fusion} fusions*!\nGreat work!");
            }
            
            await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(),
                messageBuilder.ToString(),
                ParseMode.Markdown,
                cancellationToken: cancellationToken);

            return default;
        }
    }
}