using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pokegraf.Api.WebApi.Configuration
{
    public static class CacheExtension
    {
        public static IServiceCollection AddDistributedCache(this IServiceCollection services, IConfiguration configuration)
        {
            var redisUrl = configuration["POKEGRAF_REDIS_CACHE_URL"];

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