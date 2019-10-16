using System;

namespace Pokegraf.Domain.User.Command.AddUserCommand
{
    public static class AddUserCommandExtension
    {
        public static Entity.User ExtractUserModel(this AddUserCommand request)
        {
            return new Entity.User
            {
                Id = request.UserId,
                IsBot = request.UserIsBot,
                LanguageCode = request.UserLanguageCode,
                Username = request.UserUsername,
                FirstSeen = request.Timestamp,
                TrainerName = "Trainer",
                Stats = new Entity.Stats
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    Requests = new Entity.Stats.RequestStats
                    {
                        Id = Guid.NewGuid(),
                        Fusion = 0,
                        Pokemon = 0
                    }
                }
            };
        }
    }
}