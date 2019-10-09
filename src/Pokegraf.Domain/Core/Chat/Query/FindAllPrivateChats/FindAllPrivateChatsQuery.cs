using System.Collections.Generic;

namespace Pokegraf.Domain.Core.Chat.FindAllPrivateChats
{
    public class FindAllPrivateChatsQuery : Request<Result<IEnumerable<Entity.Chat>>>
    {
        
    }
}