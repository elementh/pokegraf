using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.User.FindUser
{
    public class FindUserQueryHandler : CommonHandler<FindUserQuery, Result<Entity.User>>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public FindUserQueryHandler(ILogger<CommonHandler<FindUserQuery, Result<Entity.User>>> logger, IMediator mediatR) : base(logger, mediatR)
        {
        }

        public override async Task<Result<Entity.User>> Handle(FindUserQuery request, CancellationToken cancellationToken)
        {
            var user = await UnitOfWork.UserRepository.FindById(request.UserId);

            if (user == null)
            {
                return Result<Entity.User>.NotFound("user not found");
            }

            return Result<Entity.User>.Success(user);
        }
    }
}