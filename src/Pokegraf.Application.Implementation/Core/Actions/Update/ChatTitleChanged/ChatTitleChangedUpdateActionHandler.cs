using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Application.Implementation.Core.Actions.Update.ChatTitleChanged
{
    public class ChatTitleChangedUpdateActionHandler : CommonHandler<ChatTitleChangedUpdateAction, Result>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public ChatTitleChangedUpdateActionHandler(ILogger<CommonHandler<ChatTitleChangedUpdateAction, Result>> logger, IMediator mediatR, IUnitOfWork unitOfWork) : base(logger, mediatR)
        {
            UnitOfWork = unitOfWork;
        }

        public override async Task<Result> Handle(ChatTitleChangedUpdateAction request, CancellationToken cancellationToken)
        {
            return await MediatR.Send(request.MapToUpdateChatTitleCommand());
            
//            return await MediatR.Send(new TextResponse("TEXT"), cancellationToken);
        }
    }
}