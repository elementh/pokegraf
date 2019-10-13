using Pokegraf.Application.Implementation.Core.Responses.Text;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.Core.Responses.TextWithKeyboard
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