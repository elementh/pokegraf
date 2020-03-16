using System.Threading;
using System.Threading.Tasks;
using Pokegraf.Core.Domain.Stats.Model;

namespace Pokegraf.Core.Domain.Stats.Service
{
    public interface IGlobalStatsService
    {
        Task<GlobalStats?> Get(CancellationToken cancellationToken = default);
    }
}