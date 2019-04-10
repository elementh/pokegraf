using MediatR;

namespace Pokegraf.Application.Contract.Service
{
    public interface ITelegramService
    {
        void StartPokegrafBot();
    }
}