using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Application.Contract.Service;
using Pokegraf.Application.Implementation.BotActions.Commands.Pokemon;
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
            services.AddMediatRHandlers(configuration);
            services.AddBackgroundServices(configuration);
            services.AddServices(configuration);

            return services;
        }
        
        private static IServiceCollection AddMediatRHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(PokemonCommandActionHandler).GetTypeInfo().Assembly);

            return services;
        }
        
        private static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBotClient, BotClient>();
            services.AddScoped<ITelegramService, TelegramService>();
            services.AddSingleton<IHostedService, TelegramBackgroundService>();

            return services;
        }
                
        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<TelegramService>()
                .AddClasses(classes =>
                    classes.Where(c => c.Name.EndsWith("Service") 
                                       && c.Name != "TelegramBackgroundService" 
                                       && c.Name != "BackgroundService"))
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