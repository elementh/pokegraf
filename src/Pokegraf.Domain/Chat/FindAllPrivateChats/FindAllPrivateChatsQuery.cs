using System.Collections.Generic;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;

namespace Pokegraf.Domain.Chat.FindAllPrivateChats
{
    public class FindAllPrivateChatsQuery : Request<Result<IEnumerable<Entity.Chat>>>
    {
        
    }
}