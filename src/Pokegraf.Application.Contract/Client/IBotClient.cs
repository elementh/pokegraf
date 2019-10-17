using System.Threading;
using System.Threading.Tasks;
using MihaZupan.TelegramBotClients;

namespace Pokegraf.Application.Contract.Client
{
    public interface IBotClient
    {
        RateLimitedTelegramBotClient Client { get; }
        Task Start(CancellationToken cancellationToken = default);
    }
}