using Pokegraf.Domain.Entity;
using Pokegraf.Persistence.Contract.Context;
using Pokegraf.Persistence.Contract.Repository;

namespace Pokegraf.Persistence.Implementation.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IPokegrafDbContext dbContext) : base(dbContext)
        {
        }
    }
}