using Pokegraf.Domain.Entity;
using Pokegraf.Persistence.Contract.Context;
using Pokegraf.Persistence.Contract.Repository;

namespace Pokegraf.Persistence.Implementation.Repository
{
    public class StatsRepository : GenericRepository<Stats>, IStatsRepository
    {
        public StatsRepository(IPokegrafDbContext dbContext) : base(dbContext)
        {
        }
    }
}