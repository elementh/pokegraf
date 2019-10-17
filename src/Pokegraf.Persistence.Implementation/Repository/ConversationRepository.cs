using Pokegraf.Domain.Entity;
using Pokegraf.Persistence.Contract.Context;
using Pokegraf.Persistence.Contract.Repository;

namespace Pokegraf.Persistence.Implementation.Repository
{
    public class ConversationRepository : GenericRepository<Conversation>, IConversationRepository
    {
        public ConversationRepository(IPokegrafDbContext dbContext) : base(dbContext)
        {
        }
    }
}