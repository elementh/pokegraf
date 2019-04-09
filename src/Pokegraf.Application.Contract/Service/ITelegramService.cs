using MediatR;
using Pokegraf.Application.Contract.Event;

namespace Pokegraf.Application.Contract.Service
{
    public interface ITelegramService: INotificationHandler<IResponseRequest>
    {
        void StartPokegrafBot();
    }
}