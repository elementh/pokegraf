using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Implementation.Core.Actions.Callback.MoreFusion;
using Pokegraf.Application.Implementation.Core.Actions.Command.Fusion;
using Pokegraf.Application.Implementation.Core.Actions.Inline.Fusion;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.Stats.Command.AddOneToFusionRequests;

namespace Pokegraf.Application.Implementation.Core.Pipeline.PostHandling
{
    public class FusionStatsPostHandler : 
        IRequestPostProcessor<FusionCommandAction, Status<Error>>,
        IRequestPostProcessor<MoreFusionCallbackAction, Status<Error>>,
        IRequestPostProcessor<FusionInlineAction, Status<Error>>
    {
        protected readonly ILogger<FusionStatsPostHandler> Logger;
        protected readonly IMediator Mediator;

        public FusionStatsPostHandler(ILogger<FusionStatsPostHandler> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        public async Task Process(FusionCommandAction request, Status<Error> response, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Stats: Adding 1 to fusion requests for user: {@UserId}", request.From.Id);
            await Mediator.Send(new AddOneToFusionRequestsCommand {UserId = request.From.Id}, cancellationToken);
        }

        public async Task Process(MoreFusionCallbackAction request, Status<Error> response, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Stats: Adding 1 to fusion requests for user: {@UserId}", request.From.Id);
            await Mediator.Send(new AddOneToFusionRequestsCommand {UserId = request.From.Id}, cancellationToken);
        }

        public async Task Process(FusionInlineAction request, Status<Error> response, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Stats: Adding 1 to fusion requests for user: {@UserId}", request.From.Id);
            await Mediator.Send(new AddOneToFusionRequestsCommand {UserId = request.From.Id}, cancellationToken);
        }
    }
}