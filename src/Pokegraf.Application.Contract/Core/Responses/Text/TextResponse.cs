using Pokegraf.Application.Contract.Core.Responses;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Application.Contract.Core.Responses.Text
{
    public class TextResponse : IResponse
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