using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pokegraf.Common.IoC.Configuration
{
    public static class CacheExtension
    {
        public static IServiceCollection AddDistributedCache(this IServiceCollection services, IConfiguration configuration)
        {
            var redisUrl = configuration["POKEGRAF_REDIS_CACHE_URL"] ?? configuration.GetConnectionString("RedisCache");

            if (!string.IsNullOrWhiteSpace(redisUrl))
            {
                services.AddDistributedRedisCache(options =>
                {
                    options.Configuration = redisUrl;
                    options.InstanceName = "pokegraf_cache:";
                });
            }

            return services;
        }
    }
}