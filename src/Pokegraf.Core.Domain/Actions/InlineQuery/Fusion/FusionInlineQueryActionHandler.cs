using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Pokegraf.Core.Domain.Extensions;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Pokegraf.Infrastructure.Contract.Service;

namespace Pokegraf.Core.Domain.Actions.InlineQuery.Fusion
{
    public class FusionInlineQueryActionHandler : ActionHandler<FusionInlineQueryAction>
    {
        protected readonly IPokemonService PokemonService;

        public FusionInlineQueryActionHandler(INavigatorContext ctx, IPokemonService pokemonService) : base(ctx)
        {
            PokemonService = pokemonService;
        }

        public override async Task<Unit> Handle(FusionInlineQueryAction request, CancellationToken cancellationToken)
        {
            var fusions = new List<PokemonFusionDto>();

            for (var i = 0; i < 10; i++)
            {
                fusions.Add(PokemonService.GetFusion());
            }

            var results = fusions.Select(fusion => fusion.ToInlineQueryResultPhoto()).ToArray();

            await Ctx.Client.AnswerInlineQueryAsync(Ctx.GetInlineQuery().Id, results, 10, true, Guid.NewGuid().ToString(),
                cancellationToken: cancellationToken);

            return default;
        }
    }
}