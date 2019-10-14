using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokegraf.Application.Contract.Core.Client;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Implementation.Core.Client;
using Pokegraf.Application.Implementation.Core.Context;
using Pokegraf.Application.Implementation.Core.Service;
using Pokegraf.Application.Implementation.Core.Service.Background;
using Scrutor;

namespace Pokegraf.Application.Implementation.Configuration
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices(configuration);
            services.AddBackgroundServices(configuration);
            services.AddRequests(configuration);
            services.AddBotContext(configuration);
            
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
            services.AddScoped<IActionClient, ActionClient>();
            
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

        private static IServiceCollection AddRequests(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<TelegramService>()
                .AddClasses(classes => 
                    classes.Where(c => c.Name.EndsWith("Action")))
                .UsingRegistrationStrategy(RegistrationStrategy.Append)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }

        private static IServiceCollection AddBotContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBotContext, BotContext>();
            services.AddScoped<IStrategyContext, StrategyContext>();
            
            return services;
        }
    }
}