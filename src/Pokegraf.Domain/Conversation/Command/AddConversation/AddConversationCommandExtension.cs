using System;

namespace Pokegraf.Domain.Conversation.Command.AddConversation
{
    public static class AddConversationCommandExtension
    {
        public static Entity.Chat ExtractChatModel(this AddConversationCommand request)
        {
            return new Entity.Chat
            {
                Id = request.ChatId,
                Username = request.ChatUsername,
                Title = request.ChatTitle,
                Type = Enum.Parse<Entity.Chat.ChatType>(request.ChatType),
                FirstSeen = request.Timestamp
            };
        }

        public static Entity.User ExtractUserModel(this AddConversationCommand request)
        {
            return new Entity.User
            {
                Id = request.UserId,
                IsBot = request.UserIsBot,
                LanguageCode = request.UserLanguageCode,
                Username = request.UserUsername,
                FirstSeen = request.Timestamp,
                TrainerName = "Trainer"
            };
        }
    }
}