using System;

namespace Pokegraf.Domain.Core.Conversation.AddConversation
{
    public static class AddConversationCommandExtension
    {
        public static Entity.Chat ExtractChatModel(this AddConversationCommand addConversationCommand)
        {
            return new Entity.Chat
            {
                Id = addConversationCommand.ChatId,
                Username = addConversationCommand.ChatUsername,
                Title = addConversationCommand.ChatTitle,
                Type = Enum.Parse<Entity.Chat.ChatType>(addConversationCommand.ChatType),
                FirstSeen = addConversationCommand.Timestamp
            };
        }

        public static Entity.User ExtractUserModel(this AddConversationCommand addConversationCommand)
        {
            return new Entity.User
            {
                Id = addConversationCommand.UserId,
                IsBot = addConversationCommand.UserIsBot,
                LanguageCode = addConversationCommand.UserLanguageCode,
                Username = addConversationCommand.UserUsername,
                FirstSeen = addConversationCommand.Timestamp
            };
        }
    }
}