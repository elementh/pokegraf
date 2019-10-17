using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Stats.Command.AddOneToFusionRequests
{
    public class AddOneToFusionRequestsCommandHandler : IRequestHandler<AddOneToFusionRequestsCommand>
    {
        protected readonly ILogger<AddOneToFusionRequestsCommandHandler> Logger;
        protected readonly IUnitOfWork UnitOfWork;

        public AddOneToFusionRequestsCommandHandler(ILogger<AddOneToFusionRequestsCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddOneToFusionRequestsCommand request, CancellationToken cancellationToken)
        {
            var userStats = await UnitOfWork.StatsRepository.FindBy(st => st.UserId == request.UserId);

            userStats.Requests.Fusion += 1;
                
            try
            {
                await UnitOfWork.SaveAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error adding 1 to pokemon requests stats for user: {@UserId}", request.UserId);
            }

            return Unit.Value;
        }
    }
}