using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokegraf.Application.Implementation.Configuration;
using Pokegraf.Common.Configuration;

namespace Pokegraf.Common.IoC.Configuration
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPipelines(configuration);
            services.AddApplicationServices(configuration);

            return services;
        }
    }
}