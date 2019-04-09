using Pokegraf.Application.Contract.BotActions.Responses;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.BotActions.Responses.Text
{
    public class TextResponse : Request<Result>, IResponse
    {
        public long ChatId { get; set; }
        public string Text { get; set; }

        public TextResponse(long chatId, string text)
        {
            ChatId = chatId;
            Text = text;
        }
    }
}