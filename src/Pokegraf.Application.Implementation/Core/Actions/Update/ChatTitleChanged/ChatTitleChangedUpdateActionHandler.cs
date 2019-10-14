using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Common.ErrorHandling;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Core.Actions.Update.ChatTitleChanged
{
    public class ChatTitleChangedUpdateActionHandler : IRequestHandler<ChatTitleChangedUpdateAction, Status<Error>>
    {
        protected readonly ILogger<ChatTitleChangedUpdateActionHandler> Logger;
        protected readonly IMediator Mediator;

        public ChatTitleChangedUpdateActionHandler(ILogger<ChatTitleChangedUpdateActionHandler> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        public async Task<Status<Error>> Handle(ChatTitleChangedUpdateAction request, CancellationToken cancellationToken)
        {
            await Mediator.Send(request.MapToUpdateChatTitleCommand(), cancellationToken);

            return Ok();
        }
    }
}