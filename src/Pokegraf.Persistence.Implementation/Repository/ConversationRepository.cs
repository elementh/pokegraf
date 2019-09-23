using Pokegraf.Domain.Entity;
using Pokegraf.Persistence.Contract.Repository;
using Pokegraf.Persistence.Implementation.Context;

namespace Pokegraf.Persistence.Implementation.Repository
{
    public class ConversationRepository : GenericRepository<Conversation>, IConversationRepository
    {
        public ConversationRepository(PokegrafDbContext dbContext) : base(dbContext)
        {
        }
    }
}