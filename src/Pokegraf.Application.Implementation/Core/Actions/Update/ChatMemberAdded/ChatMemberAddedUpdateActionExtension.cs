using Pokegraf.Domain.Conversation.Command.AddConversation;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.Core.Actions.Update.ChatMemberAdded
{
    public static class ChatMemberAddedUpdateActionExtension
    {
        public static AddConversationCommand MapToAddConversationCommand(this User user, Domain.Entity.Chat chat)
        {
            return new AddConversationCommand
            {
                ChatId = chat.Id,
                ChatUsername = chat.Username,
                ChatTitle = chat.Title,
                ChatType = chat.Type.ToString(),
                UserId = user.Id,
                UserIsBot = user.IsBot,
                UserLanguageCode = user.LanguageCode,
                UserUsername = user.Username
            };
        }
    }
}