using Pokegraf.Domain.Entity;
using Pokegraf.Persistence.Contract.Repository;
using Pokegraf.Persistence.Implementation.Context;

namespace Pokegraf.Persistence.Implementation.Repository
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        public ChatRepository(PokegrafDbContext dbContext) : base(dbContext)
        {
        }
    }
}