using Pokegraf.Application.Contract.Common.Responses;
using Pokegraf.Application.Implementation.Common.Responses.Text;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.Common.Responses.TextWithKeyboard
{
    public class TextWithKeyboardResponse : TextResponse
    {
        public InlineKeyboardMarkup Keyboard { get; set; }

        public TextWithKeyboardResponse(InlineKeyboardMarkup keyboard, string text, ParseMode parseMode = ParseMode.Markdown) : base(text, parseMode)
        {
            Keyboard = keyboard;
        }
    }
}