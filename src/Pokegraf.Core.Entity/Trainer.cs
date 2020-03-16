using Navigator.Extensions.Store.Entity;

namespace Pokegraf.Core.Entity
{
    public class Trainer : User
    {
        /// <summary>
        /// Custom name for the user.
        /// </summary>
        public string TrainerName { get; set; }
    }
}