using Pokegraf.Domain.Core.User.Command.AddUserCommand;
using Pokegraf.Domain.Core.User.Query.FindUser;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.Mapping.Extension
{
    public static class InilineQueryExtension
    {
        public static FindUserQuery MapToFindUserQuery(this InlineQuery inlineQuery)
        {
            return new FindUserQuery
            {
                UserId = inlineQuery.From.Id
            };
        }
        
        public static AddUserCommand MapToAddUserCommand(this InlineQuery message)
        {
            return new AddUserCommand
            {
                UserId = message.From.Id,
                UserIsBot = message.From.IsBot,
                UserLanguageCode = message.From.LanguageCode,
                UserUsername = message.From.Username
            };
        }
    }
}