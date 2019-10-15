using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Stats.Command.AddOneToPokemonRequests
{
    public class AddOneToPokemonRequestsCommandHandler : IRequestHandler<AddOneToPokemonRequestsCommand>
    {
        protected readonly ILogger<AddOneToPokemonRequestsCommandHandler> Logger;
        protected readonly IUnitOfWork UnitOfWork;

        public AddOneToPokemonRequestsCommandHandler(ILogger<AddOneToPokemonRequestsCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(AddOneToPokemonRequestsCommand request, CancellationToken cancellationToken)
        {
            var userStats = await UnitOfWork.StatsRepository.FindBy(st => st.UserId == request.UserId);

            userStats.Requests.Pokemon += 1;
                
            try
            {
                await UnitOfWork.SaveAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error adding 1 to fusion requests stats for user: @UserId", request.UserId);
            }

            return Unit.Value;
        }
    }
}