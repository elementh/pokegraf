using Pokegraf.Application.Implementation.Core.Responses.Photo;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.Core.Responses.PhotoWithKeyboard.Send
{
    public class PhotoWithCaptionWithKeyboardResponse : PhotoWithCaptionResponse
    {
        public InlineKeyboardMarkup Keyboard { get; set; }

        public PhotoWithCaptionWithKeyboardResponse(string photo, string caption, InlineKeyboardMarkup keyboard) : base(photo, caption)
        {
            Keyboard = keyboard;
        }
    }
}