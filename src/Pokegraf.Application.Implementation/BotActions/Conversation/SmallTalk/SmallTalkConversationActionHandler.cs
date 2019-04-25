using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Common.Responses.Text;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.BotActions.Conversation.SmallTalk
{
    public class SmallTalkConversationActionHandler : Pokegraf.Common.Request.RequestHandler<SmallTalkConversationAction, Result>
    {
        public SmallTalkConversationActionHandler(ILogger<Pokegraf.Common.Request.RequestHandler<SmallTalkConversationAction, Result>> logger, IMediator mediatR) : base(logger, mediatR)
        {
        }

        public override async Task<Result> Handle(SmallTalkConversationAction request, CancellationToken cancellationToken)
        {
            return await MediatR.Send(new TextResponse(request.FulfillmentText));
        }
    }
}