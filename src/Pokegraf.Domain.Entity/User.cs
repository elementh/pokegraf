using System;
using System.Collections.Generic;

namespace Pokegraf.Domain.Entity
{
    public class User
    {
        /// <summary>
        /// Id of the user.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Specifies if the user is a bot or no.
        /// </summary>
        public bool IsBot { get; set; }
        /// <summary>
        /// Language code of the user client.
        /// </summary>
        public string LanguageCode { get; set; }
        /// <summary>
        /// Username of the user.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Date of first interaction with the user.
        /// </summary>
        public DateTime FirstSeen { get; set; }
        /// <summary>
        /// Custom name for the user.
        /// </summary>
        public string TrainerName { get; set; }
        
        /// <summary>
        /// Usage stats of the user.
        /// </summary>
        public Stats Stats { get; set; }
        public List<Conversation> Conversations { get; set; }
    }
}