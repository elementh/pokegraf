using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokegraf.Infrastructure.Contract.Service;
using Pokegraf.Infrastructure.Implementation.Service;
using Scrutor;

namespace Pokegraf.Infrastructure.Implementation.Configuration
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices(configuration);

            return services;
        }
        
        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var redisUrl = configuration["POKEGRAF_REDIS_CACHE_URL"];

            if (string.IsNullOrWhiteSpace(redisUrl))
            {
                services.AddScoped<IPokemonService, PokemonService>();
            }
            else
            {
                services.AddScoped<IPokemonService, PokemonServiceWithRedisCache>();
            }
            
//            services.Scan(scan => scan
//                .FromAssemblyOf<PokemonService>()
//                .AddClasses(classes =>
//                    classes.Where(c => c.Name.EndsWith("Service")))
//                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
//                .AsImplementedInterfaces()
//                .WithScopedLifetime());

            return services;
        }
    }
}