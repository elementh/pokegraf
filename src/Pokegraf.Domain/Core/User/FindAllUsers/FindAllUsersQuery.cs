using System.Collections.Generic;

namespace Pokegraf.Domain.Core.User.FindAllUsers
{
    public class FindAllUsersQuery : Request<Result<IEnumerable<Entity.User>>>
    {
        
    }
}