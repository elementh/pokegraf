using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Conversation.Command.AddConversation
{
    internal class AddConversationCommandHandler : IRequestHandler<AddConversationCommand>
    {
        protected readonly ILogger<AddConversationCommandHandler> Logger;
        protected readonly IUnitOfWork UnitOfWork;

        public AddConversationCommandHandler(ILogger<AddConversationCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddConversationCommand request, CancellationToken cancellationToken)
        {
            var chat = await UnitOfWork.ChatRepository.FindBy(c => c.Id == request.ChatId, c => c.Conversations) ??
                       await UnitOfWork.ChatRepository.Insert(request.ExtractChatModel());
            
            var user = await UnitOfWork.UserRepository.FindBy(u => u.Id == request.UserId, u => u.Conversations) ??
                       await UnitOfWork.UserRepository.Insert(request.ExtractUserModel());

            var conversation = new Entity.Conversation
            {
                ChatId = chat.Id,
                Chat = chat,
                UserId = user.Id,
                User = user
            };

            await UnitOfWork.ConversationRepository.Insert(conversation);

            try
            {
                await UnitOfWork.SaveAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error adding a new conversation. ChatId: {@ChatId} UserId: {@UserId}", chat.Id, user.Id);
            }

            return Unit.Value;
        }
    }
}