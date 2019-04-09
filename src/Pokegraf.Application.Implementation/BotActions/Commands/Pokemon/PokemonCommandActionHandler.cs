using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Service;

namespace Pokegraf.Application.Implementation.BotActions.Commands.Pokemon
{
    public class PokemonCommandActionHandler : Pokegraf.Common.Request.RequestHandler<PokemonCommandAction, Result>
    {
        private readonly IPokemonService _pokemonService;
        
        public PokemonCommandActionHandler(ILogger<Pokegraf.Common.Request.RequestHandler<PokemonCommandAction, Result>> logger, IMediator mediatR, IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override async Task<Result> Handle(PokemonCommandAction request, CancellationToken cancellationToken)
        {
            var result = await _pokemonService.GetPokemon(45);

            return Result.Success();
        }
    }
}