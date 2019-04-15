using Pokegraf.Application.Implementation.Common.Responses.PhotoWithKeyboard.Send;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.Common.Responses.PhotoWithKeyboard.Edit
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