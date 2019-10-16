using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.Stats.Model;
using Pokegraf.Persistence.Contract.Context;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Domain.Stats.Query.FindGlobalStats
{
    public class FindGlobalStatsQueryHandler : IRequestHandler<FindGlobalStatsQuery, Result<GlobalStats, Error>>
    {
        protected readonly ILogger<FindGlobalStatsQueryHandler> Logger;
        protected readonly IPokegrafDbContext Context;

        public FindGlobalStatsQueryHandler(ILogger<FindGlobalStatsQueryHandler> logger, IPokegrafDbContext context)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Context = context;
        }

        public async Task<Result<GlobalStats, Error>> Handle(FindGlobalStatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var pokemonRequests = await Context.Stats.AsNoTracking().SumAsync(st => st.Requests.Pokemon, cancellationToken: cancellationToken);
                var fusionRequests = await Context.Stats.AsNoTracking().SumAsync(st => st.Requests.Fusion, cancellationToken: cancellationToken);
                var usersCount = await Context.Users.AsNoTracking().CountAsync(cancellationToken);
                var chatsCount = await Context.Chats.AsNoTracking().CountAsync(ch => ch.Type == Entity.Chat.ChatType.Group || ch.Type == Entity.Chat.ChatType.Supergroup, cancellationToken: cancellationToken);

                var stats = new GlobalStats
                {
                    PokemonRequests = pokemonRequests,
                    FusionRequests = fusionRequests,
                    Users = usersCount,
                    Chats = chatsCount
                };
                
                return Ok(stats);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error calculating global stats from DbContext");

                return Error(UnknownError($"Unhandled error calculating global stats from DbContext: {e.Message}."));
            }
        }
    }
}