using Pokegraf.Application.Contract.BotActions.Responses;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.BotActions.Responses.TextWithKeyboard
{
    public class TextWithKeyboardResponse : Request<Result>, IResponse
    {
        public long ChatId { get; set; }
        public string Text { get; set; }
        public InlineKeyboardMarkup Keyboard { get; set; }

        public TextWithKeyboardResponse(long chatId, string text, InlineKeyboardMarkup keyboard)
        {
            ChatId = chatId;
            Text = text;
            Keyboard = keyboard;
        }
    }
}