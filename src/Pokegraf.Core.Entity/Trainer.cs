using Navigator.Extensions.Store.Abstraction;
using Navigator.Extensions.Store.Entity;

namespace Pokegraf.Core.Entity
{
    public class Trainer : User
    {
        /// <summary>
        /// Custom name for the user.
        /// </summary>
        public string? TrainerName { get; set; }
        /// <summary>
        /// Trainer stats.
        /// </summary>
        public Stats Stats { get; set; }
    }
    
    public class TrainerMapper : IUserMapper<Trainer>
    {
        public Trainer Parse(Telegram.Bot.Types.User user)
        {
            return new Trainer
            {
                Id = user.Id,
                IsBot = user.IsBot,
                LanguageCode = user.LanguageCode,
                Username = user.Username,
                Stats = new Stats()
            };
        }
    }
}