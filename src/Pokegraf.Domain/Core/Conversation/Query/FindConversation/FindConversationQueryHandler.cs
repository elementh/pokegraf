using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Core.Conversation.Query.FindConversation
{
    internal class FindConversationQueryHandler : CommonHandler<FindConversationQuery, Result<Entity.Conversation>>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public FindConversationQueryHandler(ILogger<CommonHandler<FindConversationQuery, Result<Entity.Conversation>>> logger, IMediator mediatR, IUnitOfWork unitOfWork) : base(logger, mediatR)
        {
            UnitOfWork = unitOfWork;
        }

        public override async Task<Result<Entity.Conversation>> Handle(FindConversationQuery request, CancellationToken cancellationToken)
        {
            var conversation = await UnitOfWork.ConversationRepository.FindBy(conv => conv.ChatId == request.ChatId && conv.UserId == request.UserId,
                conv => conv.Chat, conv => conv.User);

            if (conversation == null)
            {
                return Result<Entity.Conversation>.NotFound("conversation not found");
            }

            return Result<Entity.Conversation>.Success(conversation);
        }
    }
}