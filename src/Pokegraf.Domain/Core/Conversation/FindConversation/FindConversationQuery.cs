namespace Pokegraf.Domain.Core.Conversation.FindConversation
{
    public class FindConversationQuery : Request<Result<Entity.Conversation>>
    {
        public long ChatId { get; set; }
        public int UserId { get; set; }
    }
}