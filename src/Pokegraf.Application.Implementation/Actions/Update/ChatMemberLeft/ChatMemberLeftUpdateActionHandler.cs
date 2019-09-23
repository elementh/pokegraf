using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Common.Responses.Text;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Application.Implementation.Actions.Update.ChatMemberLeft
{
    public class ChatMemberLeftUpdateActionHandler : CommonHandler<ChatMemberLeftUpdateAction, Result>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public ChatMemberLeftUpdateActionHandler(ILogger<CommonHandler<ChatMemberLeftUpdateAction, Result>> logger, IMediator mediatR, IUnitOfWork unitOfWork) : base(logger, mediatR)
        {
            UnitOfWork = unitOfWork;
        }

        public override async Task<Result> Handle(ChatMemberLeftUpdateAction request, CancellationToken cancellationToken)
        {
            return await MediatR.Send(request.MapToDeleteConversationCommand(), cancellationToken);

//            var message = $"@{request.Update.Message.LeftChatMember.Username} left";
            
//            return await MediatR.Send(new TextResponse(message), cancellationToken);
        }
    }
}