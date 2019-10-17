using System;
using MediatR;

namespace Pokegraf.Domain.Conversation.Command.AddConversation
{
    public class AddConversationCommand : IRequest
    {
        public DateTime Timestamp { get; }

        public AddConversationCommand()
        {
            Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Id of the chat.
        /// </summary>
        public long ChatId { get; set; }
        /// <summary>
        /// Optional, available when the type of the chat is private.
        /// </summary>
        public string ChatUsername { get; set; }
        /// <summary>
        /// Optional, available when the type of the chat is a group, supergroup or channel.
        /// </summary>
        public string ChatTitle { get; set; }
        /// <summary>
        /// Type of the chat.
        /// </summary>
        public string ChatType { get; set; }
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