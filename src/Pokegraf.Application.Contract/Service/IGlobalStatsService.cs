using System.Threading.Tasks;
using OperationResult;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.Stats.Model;

namespace Pokegraf.Application.Contract.Service
{
    /// <summary>
    /// Optional middleware to get the global stats from a cached source.
    /// </summary>
    public interface IGlobalStatsService
    {
        Task<Result<GlobalStats, Error>> Get();
    }
}