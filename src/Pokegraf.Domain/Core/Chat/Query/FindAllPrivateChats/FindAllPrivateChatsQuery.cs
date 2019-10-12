using System.Collections.Generic;

namespace Pokegraf.Domain.Core.Chat.Query.FindAllPrivateChats
{
    public class FindAllPrivateChatsQuery : Request<Result<IEnumerable<Entity.Chat>>>
    {
        
    }
}