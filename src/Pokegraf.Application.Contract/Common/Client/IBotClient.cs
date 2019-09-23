using MihaZupan.TelegramBotClients;

namespace Pokegraf.Application.Contract.Common.Client
{
    public interface IBotClient
    {
        RateLimitedTelegramBotClient Client { get; }
        void Start();
    }
}