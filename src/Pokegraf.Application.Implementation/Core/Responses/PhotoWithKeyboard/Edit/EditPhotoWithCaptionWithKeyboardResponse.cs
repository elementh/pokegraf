using Pokegraf.Application.Implementation.Core.Responses.PhotoWithKeyboard.Send;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.Core.Responses.PhotoWithKeyboard.Edit
{
    public class EditPhotoWithCaptionWithKeyboardResponse : PhotoWithCaptionWithKeyboardResponse
    {
        public int MessageId { get; set; }

        public EditPhotoWithCaptionWithKeyboardResponse(string photo, string caption, InlineKeyboardMarkup keyboard, int messageId) : base(photo, caption, keyboard)
        {
            MessageId = messageId;
        }
    }
}