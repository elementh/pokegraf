using Pokegraf.Common.Request;
using Pokegraf.Common.Result;

namespace Pokegraf.Domain.Conversation.FindConversation
{
    public class FindConversationQuery : Request<Result<Entity.Conversation>>
    {
        public long ChatId { get; set; }
        public int UserId { get; set; }
    }
}