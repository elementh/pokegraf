using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Persistence.Contract.Context;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Domain.Stats.Query.FindUserStats
{
    public class FindUserStatsQueryHandler : IRequestHandler<FindUserStatsQuery, Result<Entity.Stats, Error>>
    {
        protected readonly ILogger<FindUserStatsQueryHandler> Logger;
        protected readonly IPokegrafDbContext Context;

        public FindUserStatsQueryHandler(ILogger<FindUserStatsQueryHandler> logger, IPokegrafDbContext context)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Context = context;
        }
        
        public async Task<Result<Entity.Stats, Error>> Handle(FindUserStatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var stats = await Context.Stats.AsNoTracking().FirstOrDefaultAsync(st => st.UserId == request.UserId, cancellationToken);
                
                return Ok(stats);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error calculating user stats from DbContext");

                return Error(UnknownError($"Unhandled error calculating user stats from DbContext: {e.Message}."));
            }
        }
    }
}