using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Abstraction;
using Navigator.Actions;
using Pokegraf.Infrastructure.Contract.Service;

namespace Pokegraf.Core.Domain.Actions.Command.Fusion
{
    public class FusionCommandActionHandler : ActionHandler<FusionCommandAction>
    {
        protected readonly IPokemonService PokemonService;

        public FusionCommandActionHandler(INavigatorContext ctx, IPokemonService pokemonService) : base(ctx)
        {
            PokemonService = pokemonService;
        }

        public override Task<Unit> Handle(FusionCommandAction request, CancellationToken cancellationToken)
        {
            var fusionResult = PokemonService.GetFusion();

            if (fusionResult.IsError) return Error(fusionResult.Error);

            var fusionCallback = new Dictionary<string, string>
            {
                {"action", "fusion"}
            };
        }
    }
}