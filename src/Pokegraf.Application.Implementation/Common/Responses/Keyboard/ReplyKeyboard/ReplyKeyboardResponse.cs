using Pokegraf.Application.Contract.Common.Responses;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.Common.Responses.Keyboard.ReplyKeyboard
{
    public class ReplyKeyboardResponse : Request<Result>, IResponse
    {
        public long ChatId { get; set; }
        public string Text { get; set; }
        public ReplyKeyboardMarkup Keyboard { get; set; }

        public ReplyKeyboardResponse(long chatId, string text, ReplyKeyboardMarkup keyboard)
        {
            ChatId = chatId;
            Text = text;
            Keyboard = keyboard;
        }
    }
}