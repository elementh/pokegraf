using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Chat.UpdateChatTitle
{
    internal class UpdateChatTitleCommandHandler : CommonHandler<UpdateChatTitleCommand, Result>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public UpdateChatTitleCommandHandler(ILogger<CommonHandler<UpdateChatTitleCommand, Result>> logger, IMediator mediatR, IUnitOfWork unitOfWork) : base(logger, mediatR)
        {
            UnitOfWork = unitOfWork;
        }

        public override async Task<Result> Handle(UpdateChatTitleCommand request, CancellationToken cancellationToken)
        {
            var chat = await UnitOfWork.ChatRepository.FindById(request.ChatId);

            if (chat == null) return Result.NotFound();

            chat.Title = request.Title;

            try
            {
                await UnitOfWork.SaveAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error updating chat title. ChatId: {ChatId}", chat.Id);
                
                return Result.UnknownError(new List<string> {e.Message});
            }
            
            return Result.Success();
        }
    }
}