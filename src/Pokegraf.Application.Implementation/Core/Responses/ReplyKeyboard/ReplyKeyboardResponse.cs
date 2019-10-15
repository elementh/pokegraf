using Pokegraf.Application.Contract.Core.Responses;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.Core.Responses.ReplyKeyboard
{
    public class ReplyKeyboardResponse : IResponse
    {
        public string Text { get; set; }
        public ReplyKeyboardMarkup Keyboard { get; set; }

        public ReplyKeyboardResponse(string text, ReplyKeyboardMarkup keyboard)
        {
            Text = text;
            Keyboard = keyboard;
        }
    }
}