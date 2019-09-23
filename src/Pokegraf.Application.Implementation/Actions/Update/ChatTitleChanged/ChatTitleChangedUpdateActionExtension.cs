﻿using Pokegraf.Domain.Chat.UpdateChatTitle;

 namespace Pokegraf.Application.Implementation.Actions.Update.ChatTitleChanged
{
    public static class ChatTitleChangedUpdateActionExtension
    {
        public static UpdateChatTitleCommand MapToUpdateChatTitleCommand(this ChatTitleChangedUpdateAction chatTitleChangedUpdateAction)
        {
            return new UpdateChatTitleCommand
            {
                ChatId = chatTitleChangedUpdateAction.Chat.Id,
                Title = chatTitleChangedUpdateAction.Update.Message.NewChatTitle
            };
        }
    }
}