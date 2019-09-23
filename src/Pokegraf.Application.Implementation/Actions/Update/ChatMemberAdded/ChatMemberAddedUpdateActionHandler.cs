using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Common.Responses.Text;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.Actions.Update.ChatMemberAdded
{
    public class ChatMemberAddedUpdateActionHandler : CommonHandler<ChatMemberAddedUpdateAction, Result>
    {
        public ChatMemberAddedUpdateActionHandler(ILogger<CommonHandler<ChatMemberAddedUpdateAction, Result>> logger, IMediator mediatR) : base(logger, mediatR)
        {
        }

        public override async Task<Result> Handle(ChatMemberAddedUpdateAction request, CancellationToken cancellationToken)
        {
            return Result.Success();
            
//            var newUsers = request.Update.Message.NewChatMembers.ToList();
//
//            var message = "Bienvenido a la noble causa del bolivarismo.";
//
//            if (newUsers.Count == 1)
//            {
//                message = $"Bienvenido @{newUsers.First().Username} a la noble causa del bolivarismo.";
//            }
//            else if (newUsers.Count > 1)
//            {
//                //TODO: get all usernames
//                message = $"Bienvenidos todos a la noble causa del bolivarismo.";
//            }
//
//            return await MediatR.Send(new TextResponse(message));
        }
    }
}