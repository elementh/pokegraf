using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Core.Conversation.DeleteConversation
{
    internal class DeleteConversationCommandHandler : CommonHandler<DeleteConversationCommand, Result>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public DeleteConversationCommandHandler(ILogger<CommonHandler<DeleteConversationCommand, Result>> logger, IMediator mediatR, IUnitOfWork unitOfWork) : base(logger, mediatR)
        {
            UnitOfWork = unitOfWork;
        }

        public override async Task<Result> Handle(DeleteConversationCommand request, CancellationToken cancellationToken)
        {
            var conversation = await UnitOfWork.ConversationRepository.FindBy(conv => conv.ChatId == request.ChatId && conv.UserId == request.UserId);

            if (conversation == null) return Result.NotFound();

            UnitOfWork.ConversationRepository.Delete(conversation);
            
            try
            {
                await UnitOfWork.SaveAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error deleting a conversation. ChatId: {ChatId} UserId: {UserId}", request.ChatId, request.UserId);
                
                return Result.UnknownError(new List<string> {e.Message});
            }
            
            return Result.Success();
        }
    }
}