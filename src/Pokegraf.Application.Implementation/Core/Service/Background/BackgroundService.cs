using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Core.Service.Background;

namespace Pokegraf.Application.Implementation.Core.Service.Background
{
    public abstract class BackgroundService : IBackgroundService
    {
        protected readonly ILogger<BackgroundService> Logger;
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        protected BackgroundService(ILogger<BackgroundService> logger)
        {
            Logger = logger;
        }


        protected abstract Task ExecuteAsync(CancellationToken stoppingToken);

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogDebug($"Starting background task: {this.GetType().Name}");
            // Store the task we're executing
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            // If the task is completed then return it, this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogDebug($"Stoping background task: {this.GetType().Name}");

            // Stop called without start
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                _stoppingCts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                    cancellationToken));
            }
        }
        
        public virtual void Dispose()
        {
            Logger.LogDebug($"Disposing background task: {this.GetType().Name}");
            _stoppingCts.Cancel();
        }
    }
}