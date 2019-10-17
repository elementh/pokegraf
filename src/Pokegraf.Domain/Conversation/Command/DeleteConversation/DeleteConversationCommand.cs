using MediatR;

namespace Pokegraf.Domain.Conversation.Command.DeleteConversation
{
    public class DeleteConversationCommand : IRequest
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