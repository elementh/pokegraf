using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Persistence.Contract.Context;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Domain.Core.User.Query.FindUser
{
    public class FindUserQueryHandler : IRequestHandler<FindUserQuery, Result<Entity.User, ResultError>>
    {
        protected readonly ILogger<FindUserQueryHandler> Logger;
        protected readonly IPokegrafDbContext Context;

        public FindUserQueryHandler(ILogger<FindUserQueryHandler> logger, IPokegrafDbContext context)
        {
            Logger = logger;
            Context = context;
        }

        public async Task<Result<Entity.User, ResultError>> Handle(FindUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await Context.Users.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

                if (user == null)
                {
                    return Error(NotFound("user not found"));
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error finding user from DbContext");

                return Error(UnknownError($"Unhandled error finding user from DbContext: {e.Message}."));
            }
        }
    }
}