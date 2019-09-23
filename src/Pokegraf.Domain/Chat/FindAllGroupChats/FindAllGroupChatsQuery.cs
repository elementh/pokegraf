using System.Collections.Generic;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;

namespace Pokegraf.Domain.Chat.FindAllGroupChats
{
    public class FindAllGroupChatsQuery : Request<Result<IEnumerable<Entity.Chat>>>
    {
        
    }
}