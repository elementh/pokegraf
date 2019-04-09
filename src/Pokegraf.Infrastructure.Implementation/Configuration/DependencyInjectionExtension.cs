using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.Scan(scan => scan
                .FromAssemblyOf<PokemonService>()
                .AddClasses(classes =>
                    classes.Where(c => c.Name.EndsWith("Service")))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            
            return services;
        }
    }
}