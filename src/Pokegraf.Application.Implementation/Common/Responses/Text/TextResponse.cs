using Pokegraf.Application.Contract.Common.Responses;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Application.Implementation.Common.Responses.Text
{
    public class TextResponse : Request<Result>, IResponse
    {
        public string Text { get; set; }
        public ParseMode ParseMode { get; set; }

        public TextResponse(string text, ParseMode parseMode = ParseMode.Markdown)
        {
            Text = text;
            ParseMode = parseMode;
        }
    }
}