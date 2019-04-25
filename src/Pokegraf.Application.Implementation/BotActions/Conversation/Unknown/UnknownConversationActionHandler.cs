using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Common.Responses.Text;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.BotActions.Conversation.Unknown
{
    public class UnknownConversationActionHandler : Pokegraf.Common.Request.RequestHandler<UnknownConversationAction, Result>
    {
        public UnknownConversationActionHandler(ILogger<Pokegraf.Common.Request.RequestHandler<UnknownConversationAction, Result>> logger, IMediator mediatR) : base(logger, mediatR)
        {
        }

        public override async Task<Result> Handle(UnknownConversationAction request, CancellationToken cancellationToken)
        {
            return await MediatR.Send(new TextResponse(request.FulfillmentText));
        }
    }
}