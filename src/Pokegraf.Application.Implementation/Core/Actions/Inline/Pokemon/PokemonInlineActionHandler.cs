using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Core.Responses.Inline;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.InlineQueryResults;

namespace Pokegraf.Application.Implementation.Core.Actions.Inline.Pokemon
{
    public class PokemonInlineActionHandler : Pokegraf.Common.Request.CommonHandler<PokemonInlineAction, Result>
    {
        private readonly IPokemonService _pokemonService;

        public PokemonInlineActionHandler(ILogger<Pokegraf.Common.Request.CommonHandler<PokemonInlineAction, Result>> logger,
            IMediator mediatR, IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override async Task<Result> Handle(PokemonInlineAction request, CancellationToken cancellationToken)
        {
            var requestedPokemon = request.Query.ToLower();

            var result = int.TryParse(requestedPokemon, out var pokeNumber)
                ? await _pokemonService.GetPokemon(pokeNumber)
                : await _pokemonService.GetPokemon(requestedPokemon);
            
            if (!result.Succeeded) return await MediatR.Send(new InlineResponse(new InlineQueryResultBase[]{}), cancellationToken);

            var sprite = result.Value.Sprite ?? result.Value.Image;
            
            InlineQueryResultBase[] results =
            {
                new InlineQueryResultPhoto($"pokemon:{result.Value.Id}", result.Value.Image, sprite)
                {
                    Caption = $"{result.Value.Description}"
                }
            };

            return await MediatR.Send(new InlineResponse(results), cancellationToken);
        }
    }
}