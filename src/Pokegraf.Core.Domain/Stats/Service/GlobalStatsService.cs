using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Navigator.Extensions.Store.Entity;
using Newtonsoft.Json;
using Pokegraf.Common.Helper;
using Pokegraf.Core.Domain.Stats.Model;
using Pokegraf.Persistence.Context;

namespace Pokegraf.Core.Domain.Stats.Service
{
    public class GlobalStatsService : IGlobalStatsService
    {
        protected readonly ILogger<GlobalStatsService> Logger;
        protected readonly IDistributedCache Cache;
        protected readonly PokegrafDbContext DbContext;

        public GlobalStatsService(ILogger<GlobalStatsService> logger, IDistributedCache cache, PokegrafDbContext dbContext)
        {
            Logger = logger;
            Cache = cache;
            DbContext = dbContext;
        }

        public async Task<GlobalStats?> Get(CancellationToken cancellationToken = default)
        {
            var cacheEntry = await Cache.GetStringAsync(CacheKeys.GlobalStats, cancellationToken);

            if (cacheEntry != null)
            {
                Logger.LogTrace("Global Stats were found in cache");

                return JsonConvert.DeserializeObject<GlobalStats>(cacheEntry);
            }

            Logger.LogTrace("Global Stats were not found in cache");

            GlobalStats stats;
            try
            {
                var pokemonRequests = await DbContext.Stats.AsNoTracking().SumAsync(st => st.Requests.Pokemon, cancellationToken);
                var fusionRequests = await DbContext.Stats.AsNoTracking().SumAsync(st => st.Requests.Fusion, cancellationToken);
                var usersCount = await DbContext.Users.AsNoTracking().CountAsync(cancellationToken);
                var chatsCount = await DbContext.Chats.AsNoTracking().CountAsync(ch => ch.Type == Chat.ChatType.Group || ch.Type == Chat.ChatType.Supergroup, cancellationToken);

                stats = new GlobalStats(pokemonRequests, fusionRequests, usersCount, chatsCount);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error calculating global stats.");
                return default;
            }

            // Set cache options.
            var cacheEntryOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1));

            // Save data in cache.
            await Cache.SetStringAsync(CacheKeys.GlobalStats, JsonConvert.SerializeObject(stats), cacheEntryOptions, cancellationToken);

            return stats;
        }
    }
}