using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Contract.Core.Responses.ReplyKeyboard
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