using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.User.UpdateUserTrainerName
{
    public class UpdateUserTrainerNameCommandHandler : CommonHandler<UpdateUserTrainerNameCommand, Result>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public UpdateUserTrainerNameCommandHandler(ILogger<CommonHandler<UpdateUserTrainerNameCommand, Result>> logger, IMediator mediatR, IUnitOfWork unitOfWork) : base(logger, mediatR)
        {
            UnitOfWork = unitOfWork;
        }

        public override async Task<Result> Handle(UpdateUserTrainerNameCommand request, CancellationToken cancellationToken)
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
                
                return Result.UnknownError(new List<string> {e.Message});
            }
            
            return Result.Success();
        }
    }
}