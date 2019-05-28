using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokegraf.Application.Implementation.Configuration;
using Pokegraf.Application.Implementation.Service;
using Pokegraf.Common.Configuration;
using Pokegraf.Infrastructure.Implementation.Configuration;

namespace Pokegraf.Common.IoC.Configuration
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPipelines(configuration);
            services.AddDistributedCache(configuration);
            services.AddApplicationDependencies(configuration);
            services.AddInfrastructureDependencies(configuration);

            services.AddMediatR(typeof(TelegramService).Assembly);
            
            return services;
        }
    }
}