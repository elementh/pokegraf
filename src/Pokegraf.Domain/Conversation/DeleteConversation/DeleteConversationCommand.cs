using Pokegraf.Common.Request;
using Pokegraf.Common.Result;

namespace Pokegraf.Domain.Conversation.DeleteConversation
{
    public class DeleteConversationCommand : Request<Result>
    {
        /// <summary>
        /// Id of the chat.
        /// </summary>
        public long ChatId { get; set; }
        /// <summary>
        /// Id of the user.
        /// </summary>
        public int UserId { get; set; }
    }
}