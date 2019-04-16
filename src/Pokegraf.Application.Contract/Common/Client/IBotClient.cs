using MihaZupan.TelegramBotClients;

namespace Pokegraf.Application.Contract.Common.Client
{
    public interface IBotClient
    {
        BlockingTelegramBotClient Client { get; }
        void Start();
    }
}