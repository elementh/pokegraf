using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Core.User.FindAllUsers
{
    internal class FindAllUsersQueryHandler : CommonHandler<FindAllUsersQuery, Result<IEnumerable<Entity.User>>>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public FindAllUsersQueryHandler(ILogger<CommonHandler<FindAllUsersQuery, Result<IEnumerable<Entity.User>>>> logger, IMediator mediatR, IUnitOfWork unitOfWork) : base(logger, mediatR)
        {
            UnitOfWork = unitOfWork;
        }

        public override async Task<Result<IEnumerable<Entity.User>>> Handle(FindAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await UnitOfWork.UserRepository.GetAll();

            return Result<IEnumerable<Entity.User>>.Success(users);
        }
    }
}