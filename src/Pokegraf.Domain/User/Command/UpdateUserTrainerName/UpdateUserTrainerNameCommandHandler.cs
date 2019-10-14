using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.User.Command.UpdateUserTrainerName
{
    public class UpdateUserTrainerNameCommandHandler : IRequestHandler<UpdateUserTrainerNameCommand>
    {
        protected readonly ILogger<UpdateUserTrainerNameCommand> Logger;
        protected readonly IUnitOfWork UnitOfWork;

        public UpdateUserTrainerNameCommandHandler(ILogger<UpdateUserTrainerNameCommand> logger, IUnitOfWork unitOfWork)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserTrainerNameCommand request, CancellationToken cancellationToken)
        {
            var user = await UnitOfWork.UserRepository.FindById(request.UserId);

            user.TrainerName = request.TrainerName;
            
            try
            {
                await UnitOfWork.SaveAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error updating user trainer name. UserId: {UserId}", user.Id);
            }
            
            return Unit.Value;
        }
    }
}