using Pokegraf.Application.Contract.Event;

namespace Pokegraf.Application.Implementation.Event
{
    public class TextResponseRequest : IResponseRequest
    {
        public long ChatId { get; set; }
        public string Text { get; set; }
    }
}