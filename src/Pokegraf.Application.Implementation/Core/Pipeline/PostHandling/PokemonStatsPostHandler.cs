using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Implementation.Core.Actions.Callback.PokemonBefore;
using Pokegraf.Application.Implementation.Core.Actions.Callback.PokemonNext;
using Pokegraf.Application.Implementation.Core.Actions.Command.Pokemon;
using Pokegraf.Application.Implementation.Core.Actions.Inline.Pokemon;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.Stats.Command.AddOneToPokemonRequests;

namespace Pokegraf.Application.Implementation.Core.Pipeline.PostHandling
{
    public class PokemonStatsPostHandler :
        IRequestPostProcessor<PokemonCommandAction, Status<Error>>,
        IRequestPostProcessor<PokemonInlineAction, Status<Error>>,
        IRequestPostProcessor<PokemonNextCallbackAction, Status<Error>>,
        IRequestPostProcessor<PokemonBeforeCallbackAction, Status<Error>>
    {
        protected readonly ILogger<FusionStatsPostHandler> Logger;
        protected readonly IMediator Mediator;

        public PokemonStatsPostHandler(ILogger<FusionStatsPostHandler> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        public async Task Process(PokemonCommandAction request, Status<Error> response, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Stats: Adding 1 to pokemon requests for user: {@UserId}", request.From.Id);
            await Mediator.Send(new AddOneToPokemonRequestsCommand {UserId = request.From.Id}, cancellationToken);
        }

        public async Task Process(PokemonInlineAction request, Status<Error> response, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Stats: Adding 1 to pokemon requests for user: {@UserId}", request.From.Id);
            await Mediator.Send(new AddOneToPokemonRequestsCommand {UserId = request.From.Id}, cancellationToken);
        }

        public async Task Process(PokemonNextCallbackAction request, Status<Error> response, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Stats: Adding 1 to pokemon requests for user: {@UserId}", request.From.Id);
            await Mediator.Send(new AddOneToPokemonRequestsCommand {UserId = request.From.Id}, cancellationToken);
        }

        public async Task Process(PokemonBeforeCallbackAction request, Status<Error> response, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Stats: Adding 1 to pokemon requests for user: {@UserId}", request.From.Id);
            await Mediator.Send(new AddOneToPokemonRequestsCommand {UserId = request.From.Id}, cancellationToken);
        }
    }
}