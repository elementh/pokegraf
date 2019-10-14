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

namespace Pokegraf.Domain.Core.Chat.Query.FindAllPrivateChats
{
    internal class FindAllPrivateChatsQueryHandler : IRequestHandler<FindAllPrivateChatsQuery, Result<IEnumerable<Entity.Chat>, ResultError>>
    {
        protected readonly ILogger<FindAllPrivateChatsQueryHandler> Logger;
        protected readonly IPokegrafDbContext Context;

        public FindAllPrivateChatsQueryHandler(ILogger<FindAllPrivateChatsQueryHandler> logger, IPokegrafDbContext context)
        {
            Logger = logger;
            Context = context;
        }

        public async Task<Result<IEnumerable<Entity.Chat>, ResultError>> Handle(FindAllPrivateChatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var groupChats = await Context.Chats.AsNoTracking().
                    Where(c => c.Type == Entity.Chat.ChatType.Private)
                    .ToListAsync(cancellationToken);

                return Ok(groupChats.AsEnumerable());
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error reading chats from DbContext");

                return Error(UnknownError($"Unhandled error reading chats from DbContext: {e.Message}."));
            }
        }
    }
}