namespace Pokegraf.Domain.Core.User.AddUser
{
    public static class AddUserCommandExtension
    {
        public static Entity.User ExtractUserModel(this AddUserCommand addUserCommand)
        {
            return new Entity.User
            {
                Id = addUserCommand.UserId,
                IsBot = addUserCommand.UserIsBot,
                LanguageCode = addUserCommand.UserLanguageCode,
                Username = addUserCommand.UserUsername,
                FirstSeen = addUserCommand.Timestamp
            };
        }
    }
}