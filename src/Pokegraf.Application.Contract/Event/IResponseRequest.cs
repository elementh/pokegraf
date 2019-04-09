using MediatR;

namespace Pokegraf.Application.Contract.Event
{
    public interface IResponseRequest : INotification
    {
        long ChatId { get; set; }
    }
}