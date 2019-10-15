using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Contract.Core.Responses.Text.WithKeyboard.Edit
{
    public class TextWithKeyboardEditResponse : TextWithKeyboardResponse
    {
        public int MessageId { get; set; }

        public TextWithKeyboardEditResponse(int messageId, InlineKeyboardMarkup keyboard, string text, ParseMode parseMode = ParseMode.Markdown) : base(keyboard, text, parseMode)
        {
            MessageId = messageId;
        }
    }
}