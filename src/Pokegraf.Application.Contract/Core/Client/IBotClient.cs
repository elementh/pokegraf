using MihaZupan.TelegramBotClients;

namespace Pokegraf.Application.Contract.Core.Client
{
    public interface IBotClient
    {
        RateLimitedTelegramBotClient Client { get; }
        void Start();
    }
}