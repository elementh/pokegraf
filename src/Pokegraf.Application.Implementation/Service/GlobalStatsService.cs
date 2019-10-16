using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Service;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Common.Helper;
using Pokegraf.Domain.Stats.Model;
using Pokegraf.Domain.Stats.Query.FindGlobalStats;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Service
{
    public class GlobalStatsService : IGlobalStatsService
    {
        protected readonly ILogger<GlobalStatsServiceWithRedisCache> Logger;
        protected readonly IMediator Mediator;

        public GlobalStatsService(ILogger<GlobalStatsServiceWithRedisCache> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        public async Task<Result<GlobalStats, Error>> Get(CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(new FindGlobalStatsQuery(), cancellationToken);

            if (result.IsError)
            {
                return Error(result.Error);
            }

            return Ok(result.Value);
        }
    }
}