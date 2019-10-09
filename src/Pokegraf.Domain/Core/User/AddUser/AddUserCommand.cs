namespace Pokegraf.Domain.Core.User.AddUser
{
    public class AddUserCommand : Request<Result>
    {
        /// <summary>
        /// Id of the user.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Specifies if the user is a bot or no.
        /// </summary>
        public bool UserIsBot { get; set; }
        /// <summary>
        /// Language code of the user client.
        /// </summary>
        public string UserLanguageCode { get; set; }
        /// <summary>
        /// Username of the user.
        /// </summary>
        public string UserUsername { get; set; }
    }
}