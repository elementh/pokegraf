using Pokegraf.Domain.Conversation.AddConversation;
using Pokegraf.Domain.Conversation.FindConversation;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.Mapping.Extension
{
    public static class MessageExtension
    {
        public static FindConversationQuery MapToFindConversationQuery(this Message message)
        {
            return new FindConversationQuery
            {
                ChatId = message.Chat.Id,
                UserId = message.From.Id
            };
        }

        public static AddConversationCommand MapToAddConversationCommand(this Message message)
        {
            return new AddConversationCommand
            {
                ChatId = message.Chat.Id,
                ChatUsername = message.Chat.Username,
                ChatTitle = message.Chat.Title,
                ChatType = message.Chat.Type.ToString(),
                UserId = message.From.Id,
                UserIsBot = message.From.IsBot,
                UserLanguageCode = message.From.LanguageCode,
                UserUsername = message.From.Username
            };
        }
    }
}