using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.User.Command.AddUserCommand
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
    {
        protected readonly ILogger<AddUserCommandHandler> Logger;
        protected readonly IUnitOfWork UnitOfWork;

        public AddUserCommandHandler(ILogger<AddUserCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await UnitOfWork.UserRepository.FindBy(u => u.Id == request.UserId) ??
                       await UnitOfWork.UserRepository.Insert(request.ExtractUserModel());
            
            try
            {
                await UnitOfWork.SaveAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error adding a new user: @UserId", user.Id);
            }

            return Unit.Value;
        }
    }
}