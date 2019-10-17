using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Common.ErrorHandling;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Core.Actions.Update.ChatMemberAdded
{
    public class ChatMemberAddedUpdateActionHandler : IRequestHandler<ChatMemberAddedUpdateAction, Status<Error>>
    {
        protected readonly ILogger<ChatMemberAddedUpdateActionHandler> Logger;
        protected readonly IMediator Mediator;

        public ChatMemberAddedUpdateActionHandler(ILogger<ChatMemberAddedUpdateActionHandler> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        public async Task<Status<Error>> Handle(ChatMemberAddedUpdateAction request, CancellationToken cancellationToken)
        {
            var newUsers = request.Update.Message.NewChatMembers.ToList();

            foreach (var user in newUsers)
            {
                await Mediator.Send(user.MapToAddConversationCommand(request.Chat), cancellationToken);
            }

            return Ok();
        }
    }
}