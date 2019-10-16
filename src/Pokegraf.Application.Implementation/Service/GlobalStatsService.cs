using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Service;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Common.Helper;
using Pokegraf.Domain.Stats.Model;
using Pokegraf.Domain.Stats.Query.FindGlobalStats;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Service
{
    public class GlobalStatsService : IGlobalStatsService
    {
        protected readonly ILogger<GlobalStatsService> Logger;
        protected readonly IMediator Mediator;
        protected readonly IMemoryCache Cache;

        public GlobalStatsService(ILogger<GlobalStatsService> logger, IMediator mediator, IMemoryCache cache)
        {
            Logger = logger;
            Mediator = mediator;
            Cache = cache;
        }

        public async Task<Result<GlobalStats, Error>> Get(CancellationToken cancellationToken = default)
        {
            GlobalStats cacheEntry;
            
            if (!Cache.TryGetValue(CacheKeys.GlobalStats, out cacheEntry))
            {
                // Key not in cache, so get data.
                var result = await Mediator.Send(new FindGlobalStatsQuery(), cancellationToken);

                if (result.IsError)
                {
                    return Error(result.Error);
                }

                cacheEntry = result.Value;
                
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1));

                // Save data in cache.
                Cache.Set(CacheKeys.GlobalStats, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }
    }
}