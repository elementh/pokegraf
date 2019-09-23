using Pokegraf.Common.Request;
using Pokegraf.Common.Result;

namespace Pokegraf.Domain.User.FindUser
{
    public class FindUserQuery : Request<Result<Entity.User>>
    {
        public int UserId { get; set; }
    }
}