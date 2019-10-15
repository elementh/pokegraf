﻿using Telegram.Bot.Types.ReplyMarkups;

 namespace Pokegraf.Application.Implementation.Core.Responses.Photo.WithKeyboard.Edit
{
    public class PhotoWithKeyboardEditResponse : PhotoWithKeyboardResponse
    {
        public int MessageId { get; set; }

        public PhotoWithKeyboardEditResponse(string photo, string caption, InlineKeyboardMarkup keyboard, int messageId) : base(photo, caption, keyboard)
        {
            MessageId = messageId;
        }
    }
}