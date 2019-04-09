using MediatR;
using Pokegraf.Application.Contract.Event;

namespace Pokegraf.Application.Contract.Service
{
    public interface ITelegramService
    {
        void StartPokegrafBot();
    }
}