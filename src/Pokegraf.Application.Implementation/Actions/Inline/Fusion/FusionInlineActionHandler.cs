using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Common.Responses.Inline;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.InlineQueryResults;

namespace Pokegraf.Application.Implementation.Actions.Inline.Fusion
{
    public class FusionInlineActionHandler : Pokegraf.Common.Request.CommonHandler<FusionInlineAction, Result>
    {
        private readonly IPokemonService _pokemonService;

        public FusionInlineActionHandler(ILogger<Pokegraf.Common.Request.CommonHandler<FusionInlineAction, Result>> logger, IMediator mediatR, IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override async Task<Result> Handle(FusionInlineAction request, CancellationToken cancellationToken)
        {
            var fusions = new List<PokemonFusionDto>();

            for (var i = 0; i < 50; i++)
            {
                var fusionResult = _pokemonService.GetFusion();

                if (fusionResult.IsSuccess) fusions.Add(fusionResult.Value);
            }

            InlineQueryResultBase[] results = fusions.Select(fusion => fusion.ToInlineQueryResultPhoto()).ToArray();

            return await MediatR.Send(new InlineResponse(results), cancellationToken);
        }
    }
}