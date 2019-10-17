using MediatR;
using OperationResult;
using Pokegraf.Common.ErrorHandling;

namespace Pokegraf.Domain.Stats.Query.FindUserStats
{
    public class FindUserStatsQuery : IRequest<Result<Entity.Stats, Error>>
    {
        public int UserId { get; set; }
    }
}