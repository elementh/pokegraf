using MihaZupan.TelegramBotClients;

namespace Pokegraf.Application.Contract.Client
{
    public interface IBotClient
    {
        BlockingTelegramBotClient Client { get; }
        void Start();
    }
}