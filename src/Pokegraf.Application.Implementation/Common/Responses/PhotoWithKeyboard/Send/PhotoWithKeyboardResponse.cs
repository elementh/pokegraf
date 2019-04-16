using Pokegraf.Application.Implementation.Common.Responses.Photo;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.Common.Responses.PhotoWithKeyboard.Send
{
    public class PhotoWithKeyboardResponse : PhotoResponse
    {
        public InlineKeyboardMarkup Keyboard { get; set; }

        public PhotoWithKeyboardResponse(string photo, InlineKeyboardMarkup keyboard) : base(photo)
        {
            Keyboard = keyboard;
        }
    }
}