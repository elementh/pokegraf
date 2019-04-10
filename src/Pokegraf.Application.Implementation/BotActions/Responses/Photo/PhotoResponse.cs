using Pokegraf.Application.Contract.BotActions.Responses;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.BotActions.Responses.Photo
{
    public class PhotoResponse : Request<Result>, IResponse
    {
        public long ChatId { get; set; }
        public string Photo { get; set; }

        public PhotoResponse(long chatId, string photo)
        {
            ChatId = chatId;
            Photo = photo;
        }
    }
}