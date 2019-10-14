using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Pokegraf.Domain.User.Query.FindAllUsers
{
    internal class FindAllUsersQueryHandler : IRequestHandler<FindAllUsersQuery, Result<IEnumerable<Entity.User>, ResultError>>
    {
        protected readonly ILogger<FindAllUsersQueryHandler> Logger;
        protected readonly IPokegrafDbContext Context;

        public FindAllUsersQueryHandler(ILogger<FindAllUsersQueryHandler> logger, IPokegrafDbContext context)
        {
            Logger = logger;
            Context = context;
        }

        public async Task<Result<IEnumerable<Entity.User>, ResultError>> Handle(FindAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await Context.Users.AsNoTracking().ToListAsync(cancellationToken);

                return Ok(users.AsEnumerable());
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error reading users from DbContext");

                return Error(UnknownError($"Unhandled error reading users from DbContext: {e.Message}."));
            }
        }
    }
}