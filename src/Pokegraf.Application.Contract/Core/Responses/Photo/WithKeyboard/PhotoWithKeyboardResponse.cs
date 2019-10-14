using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Contract.Core.Responses.Photo.WithKeyboard
{
    public class PhotoWithKeyboardResponse : PhotoResponse
    {
        public InlineKeyboardMarkup Keyboard { get; set; }

        public PhotoWithKeyboardResponse(string photo, string caption, InlineKeyboardMarkup keyboard) : base(photo, caption)
        {
            Keyboard = keyboard;
        }
    }
}