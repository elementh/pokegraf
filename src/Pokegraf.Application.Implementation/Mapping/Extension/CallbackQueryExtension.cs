using Pokegraf.Domain.Conversation.Command.AddConversation;
using Pokegraf.Domain.Conversation.Query.FindConversation;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.Mapping.Extension
{
    public static class CallbackQueryExtension
    {
        public static FindConversationQuery MapToFindConversationQuery(this CallbackQuery callbackQuery)
        {
            return new FindConversationQuery
            {
                ChatId = callbackQuery.Message.Chat.Id,
                UserId = callbackQuery.From.Id
            };
        }

        public static AddConversationCommand MapToAddConversationCommand(this CallbackQuery callbackQuery)
        {
            return new AddConversationCommand
            {
                ChatId = callbackQuery.Message.Chat.Id,
                ChatUsername = callbackQuery.Message.Chat.Username,
                ChatTitle = callbackQuery.Message.Chat.Title,
                ChatType = callbackQuery.Message.Chat.Type.ToString(),
                UserId = callbackQuery.From.Id,
                UserIsBot = callbackQuery.From.IsBot,
                UserLanguageCode = callbackQuery.From.LanguageCode,
                UserUsername = callbackQuery.From.Username
            };
        }
    }
}