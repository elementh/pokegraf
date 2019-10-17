﻿using Pokegraf.Domain.Chat.Command.UpdateChatTitle;

 namespace Pokegraf.Application.Implementation.Core.Actions.Update.ChatTitleChanged
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