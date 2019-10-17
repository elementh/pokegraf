using MediatR;
using OperationResult;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.Stats.Model;

namespace Pokegraf.Domain.Stats.Query.FindGlobalStats
{
    public class FindGlobalStatsQuery : IRequest<Result<GlobalStats, Error>>
    {
        
    }
}