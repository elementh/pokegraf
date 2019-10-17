using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Chat.Command.UpdateChatTitle
{
    internal class UpdateChatTitleCommandHandler : IRequestHandler<UpdateChatTitleCommand>
    {
        protected readonly ILogger<UpdateChatTitleCommandHandler> Logger;
        protected readonly IUnitOfWork UnitOfWork;

        public UpdateChatTitleCommandHandler(ILogger<UpdateChatTitleCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateChatTitleCommand request, CancellationToken cancellationToken)
        {
            var chat = await UnitOfWork.ChatRepository.FindById(request.ChatId);

            if (chat == null)
            {
                return Unit.Value;
            }

            chat.Title = request.Title;

            try
            {
                await UnitOfWork.SaveAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error updating chat title. ChatId: {@ChatId}", chat.Id);
            }

            return Unit.Value;
        }
    }
}