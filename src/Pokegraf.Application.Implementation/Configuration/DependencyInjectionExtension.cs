using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Application.Implementation.Client;
using Pokegraf.Application.Implementation.Service;
using Pokegraf.Application.Implementation.Service.Background;
using Scrutor;

namespace Pokegraf.Application.Implementation.Configuration
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices(configuration);
            services.AddBackgroundServices(configuration);

            return services;
        }
        
        private static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBotClient, BotClient>();
            services.AddSingleton<IHostedService, TelegramBackgroundService>();

            return services;
        }
                
        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<TelegramService>()
                .AddClasses(classes =>
                    classes.Where(c => c.Name.EndsWith("Service") 
                                       && c.Name != "TelegramBackgroundService"))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<TelegramService>()
                .AddClasses(classes =>
                    classes.Where(c => c.Name.EndsWith("Factory")))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            
            return services;
        }
    }
}