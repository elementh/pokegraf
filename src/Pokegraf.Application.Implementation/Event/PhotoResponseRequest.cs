using System;
using Pokegraf.Application.Contract.Event;
using Telegram.Bot.Requests;

namespace Pokegraf.Application.Implementation.Event
{
    public class PhotoResponseRequest : IResponseRequest
    {
        public long ChatId { get; set; }
        public Uri Photo { get; set; }
        public string Caption { get; set; }
    }
}