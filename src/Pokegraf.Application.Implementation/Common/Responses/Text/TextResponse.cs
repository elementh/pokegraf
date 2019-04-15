using Pokegraf.Application.Contract.Common.Responses;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Application.Implementation.Common.Responses.Text
{
    public class TextResponse : Request<Result>, IResponse
    {
        public long ChatId { get; set; }
        public string Text { get; set; }
        public ParseMode ParseMode { get; set; }

        public TextResponse(long chatId, string text, ParseMode parseMode = ParseMode.Markdown)
        {
            ChatId = chatId;
            Text = text;
            ParseMode = parseMode;
        }
    }
}