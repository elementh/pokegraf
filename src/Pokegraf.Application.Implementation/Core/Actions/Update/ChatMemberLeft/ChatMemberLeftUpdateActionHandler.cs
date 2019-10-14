using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Implementation.Core.Actions.Update.ChatMemberAdded;
using Pokegraf.Common.ErrorHandling;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Core.Actions.Update.ChatMemberLeft
{
    public class ChatMemberLeftUpdateActionHandler : IRequestHandler<ChatMemberLeftUpdateAction, Status<ResultError>>
    {
        protected readonly ILogger<ChatMemberAddedUpdateActionHandler> Logger;
        protected readonly IMediator Mediator;

        public ChatMemberLeftUpdateActionHandler(ILogger<ChatMemberAddedUpdateActionHandler> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        public async Task<Status<ResultError>> Handle(ChatMemberLeftUpdateAction request, CancellationToken cancellationToken)
        {
            await Mediator.Send(request.MapToDeleteConversationCommand(), cancellationToken);

            return Ok();
        }
    }
}