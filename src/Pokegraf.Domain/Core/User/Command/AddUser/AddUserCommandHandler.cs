using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Core.User.AddUser
{
    internal class AddUserCommandHandler : CommonHandler<AddUserCommand, Result>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public AddUserCommandHandler(ILogger<CommonHandler<AddUserCommand, Result>> logger, IMediator mediatR, IUnitOfWork unitOfWork) : base(logger, mediatR)
        {
            UnitOfWork = unitOfWork;
        }

        public override async Task<Result> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await UnitOfWork.UserRepository.Insert(request.ExtractUserModel());
            
            try
            {
                await UnitOfWork.SaveAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error adding a new user. UserId: {UserId}", user.Id);
                
                return Result.UnknownError(new List<string> {e.Message});
            }
            
            return Result.Success();
        }
    }
}