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

namespace Pokegraf.Domain.Conversation.Query.FindConversation
{
    internal class FindConversationQueryHandler : IRequestHandler<FindConversationQuery, Result<Entity.Conversation, ResultError>>
    {
        protected readonly ILogger<FindConversationQueryHandler> Logger;
        protected readonly IPokegrafDbContext Context;
        
        public async Task<Result<Entity.Conversation, ResultError>> Handle(FindConversationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var conversation = await Context.Conversations.AsNoTracking()
                    .Include(conv => conv.Chat)
                    .Include(conv => conv.User)
                    .FirstOrDefaultAsync(conv => conv.ChatId == request.ChatId && conv.UserId == request.UserId, cancellationToken);

                if (conversation == null)
                {
                    return Error(NotFound("conversation not found"));
                }

                return Ok(conversation);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error finding conversations from DbContext");

                return Error(UnknownError($"Unhandled error finding conversations from DbContext: {e.Message}."));
            }
        }
    }
}