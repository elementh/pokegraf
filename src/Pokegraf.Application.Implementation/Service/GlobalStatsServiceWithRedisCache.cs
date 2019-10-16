using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OperationResult;
using Pokegraf.Application.Contract.Service;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Common.Helper;
using Pokegraf.Domain.Stats.Model;
using Pokegraf.Domain.Stats.Query.FindGlobalStats;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Service
{
    public class GlobalStatsServiceWithRedisCache : IGlobalStatsService
    {
        protected readonly ILogger<GlobalStatsServiceWithRedisCache> Logger;
        protected readonly IMediator Mediator;
        protected readonly IDistributedCache Cache;

        public GlobalStatsServiceWithRedisCache(ILogger<GlobalStatsServiceWithRedisCache> logger, IMediator mediator, IDistributedCache cache)
        {
            Logger = logger;
            Mediator = mediator;
            Cache = cache;
        }

        public async Task<Result<GlobalStats, Error>> Get(CancellationToken cancellationToken = default)
        {
            // Try to find data in cache.
            var cacheEntry = await Cache.GetStringAsync(CacheKeys.GlobalStats, cancellationToken);

            if (cacheEntry != null)
            {
                Logger.LogTrace("Global Stats were found in cache");
                
                return Ok(JsonConvert.DeserializeObject<GlobalStats>(cacheEntry));
            }
            
            Logger.LogTrace("Global Stats was not found in cache");

            var result = await Mediator.Send(new FindGlobalStatsQuery(), cancellationToken);
            if (result.IsError)
            {
                return Error(result.Error);
            }

            cacheEntry = JsonConvert.SerializeObject(result.Value);
                
            // Set cache options.
            var cacheEntryOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1));

            // Save data in cache.
            await Cache.SetStringAsync(CacheKeys.GlobalStats, cacheEntry, cacheEntryOptions, cancellationToken);

            return Ok(result.Value);
        }
    }
}