using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Responses.Inline;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.InlineQueryResults;

namespace Pokegraf.Application.Implementation.Core.Actions.Inline.Fusion
{
    public class FusionInlineActionHandler : IRequestHandler<FusionInlineAction, Status<Error>>
    {
        protected readonly ILogger<FusionInlineActionHandler> Logger;
        protected readonly IMediator Mediator;
        protected readonly IPokemonService PokemonService;

        public FusionInlineActionHandler(ILogger<FusionInlineActionHandler> logger, IMediator mediator, IPokemonService pokemonService)
        {
            Logger = logger;
            Mediator = mediator;
            PokemonService = pokemonService;
        }


        public async Task<Status<Error>> Handle(FusionInlineAction request, CancellationToken cancellationToken)
        {
            var fusions = new List<PokemonFusionDto>();

            for (var i = 0; i < 50; i++)
            {
                var fusionResult = PokemonService.GetFusion();

                if (fusionResult.IsSuccess) fusions.Add(fusionResult.Value);
            }

            InlineQueryResultBase[] results = fusions.Select(fusion => fusion.ToInlineQueryResultPhoto()).ToArray();

            return await Mediator.Send(new InlineResponse(results), cancellationToken);
        }
    }
}