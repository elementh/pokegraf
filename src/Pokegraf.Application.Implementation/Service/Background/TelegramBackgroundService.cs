using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Service;

namespace Pokegraf.Application.Implementation.Service.Background
{
    public class TelegramBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TelegramBackgroundService(ILogger<BackgroundService> logger, IServiceScopeFactory serviceScopeFactory) : base(logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var botService = scope.ServiceProvider.GetRequiredService<ITelegramService>();
                
            await botService.StartPokegrafBot(stoppingToken);
        }
    }
}