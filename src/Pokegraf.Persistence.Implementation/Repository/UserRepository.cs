using Pokegraf.Domain.Entity;
using Pokegraf.Persistence.Contract.Repository;
using Pokegraf.Persistence.Implementation.Context;

namespace Pokegraf.Persistence.Implementation.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PokegrafDbContext dbContext) : base(dbContext)
        {
        }
    }
}