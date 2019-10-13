using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Core.Conversation.Command.DeleteConversation
{
    internal class DeleteConversationCommandHandler : IRequestHandler<DeleteConversationCommand>
    {
        protected readonly ILogger<DeleteConversationCommandHandler> Logger;
        protected readonly IUnitOfWork UnitOfWork;

        public DeleteConversationCommandHandler(ILogger<DeleteConversationCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteConversationCommand request, CancellationToken cancellationToken)
        {
            var conversation = await UnitOfWork.ConversationRepository.FindBy(conv => conv.ChatId == request.ChatId && conv.UserId == request.UserId);

            if (conversation == null)
            {
                return Unit.Value;
            }
            
            UnitOfWork.ConversationRepository.Delete(conversation);
            
            try
            {
                await UnitOfWork.SaveAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error deleting a conversation. ChatId: {ChatId} UserId: {UserId}", request.ChatId, request.UserId);
            }

            return Unit.Value;
        }
    }
}