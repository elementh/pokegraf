using Pokegraf.Application.Implementation.BotActions.Responses.Photo;
using Pokegraf.Application.Implementation.BotActions.Responses.PhotoWithKeyboard.Send;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.BotActions.Responses.PhotoWithKeyboard.Edit
{
    public class EditPhotoWithCaptionWithKeyboardResponse : PhotoWithCaptionWithKeyboardResponse
    {
        public int MessageId { get; set; }

        public EditPhotoWithCaptionWithKeyboardResponse(long chatId, string photo, string caption, InlineKeyboardMarkup keyboard, int messageId) : base(chatId, photo, caption, keyboard)
        {
            MessageId = messageId;
        }
    }
}