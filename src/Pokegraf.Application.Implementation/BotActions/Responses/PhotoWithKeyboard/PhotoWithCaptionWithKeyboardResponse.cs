using Pokegraf.Application.Implementation.BotActions.Responses.Photo;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.BotActions.Responses.PhotoWithKeyboard
{
    public class PhotoWithCaptionWithKeyboardResponse : PhotoWithCaptionResponse
    {
        public InlineKeyboardMarkup Keyboard { get; set; }

        public PhotoWithCaptionWithKeyboardResponse(long chatId, string photo, string caption, InlineKeyboardMarkup keyboard) : base(chatId, photo, caption)
        {
            Keyboard = keyboard;
        }
    }
}