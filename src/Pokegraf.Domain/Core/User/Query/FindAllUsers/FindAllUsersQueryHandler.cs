using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OperationResult;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Core.User.Query.FindAllUsers
{
    internal class FindAllUsersQueryHandler : IRequestHandler<FindAllUsersQuery, Result<IEnumerable<Entity.User>, ResultError>>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public Task<Result<IEnumerable<Entity.User>, ResultError>> Handle(FindAllUsersQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}