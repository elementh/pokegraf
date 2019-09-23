using System.Collections.Generic;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;

namespace Pokegraf.Domain.Chat.FindAllSuperGroupChats
{
    public class FindAllSuperGroupChatsQuery : Request<Result<IEnumerable<Entity.Chat>>>
    {
        
    }
}