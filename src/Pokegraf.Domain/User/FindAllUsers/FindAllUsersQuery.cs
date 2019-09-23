using System.Collections.Generic;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;

namespace Pokegraf.Domain.User.FindAllUsers
{
    public class FindAllUsersQuery : Request<Result<IEnumerable<Entity.User>>>
    {
        
    }
}