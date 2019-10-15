using System.Threading;
using System.Threading.Tasks;

namespace Pokegraf.Application.Contract.Service
{
    public interface ITelegramService
    {
        Task StartPokegrafBot(CancellationToken stoppingToken = default);
    }
}