using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Pokegraf.Application.Contract.Service;

namespace Pokegraf.Application.Implementation.Background
{
    public class TelegramBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TelegramBackgroundService(IServiceScopeFactory serviceScopeFactory) : base()
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var botService = scope.ServiceProvider.GetRequiredService<ITelegramService>();
                
                botService.StartPokegrafBot();
            }

            return Task.CompletedTask;
        }
    }
}