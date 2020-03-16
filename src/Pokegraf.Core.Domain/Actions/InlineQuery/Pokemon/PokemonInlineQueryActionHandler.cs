using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Pokegraf.Core.Domain.Extensions;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.InlineQueryResults;

namespace Pokegraf.Core.Domain.Actions.InlineQuery.Pokemon
{
    public class PokemonInlineQueryActionHandler : ActionHandler<PokemonInlineQueryAction>
    {
        protected readonly IPokemonService PokemonService;
        public PokemonInlineQueryActionHandler(INavigatorContext ctx, IPokemonService pokemonService) : base(ctx)
        {
            PokemonService = pokemonService;
        }

        public override async Task<Unit> Handle(PokemonInlineQueryAction request, CancellationToken cancellationToken)
        {
            var requestedPokemon = request.Query.ToLower();

            var pokemon = int.TryParse(requestedPokemon, out var pokeNumber)
                ? await PokemonService.GetPokemon(pokeNumber)
                : await PokemonService.GetPokemon(requestedPokemon);

            if (pokemon == null)
            {
                await Ctx.Client.AnswerInlineQueryAsync(Ctx.GetInlineQuery().Id, new InlineQueryResultBase[]{}, 
                    10, true, Guid.NewGuid().ToString(), cancellationToken: cancellationToken);

                return default;
            }
            
            InlineQueryResultBase[] results =
            {
                pokemon.ToInlineQueryResultPhoto()
            };

            await Ctx.Client.AnswerInlineQueryAsync(Ctx.GetInlineQuery().Id, results, 3600, false,
                cancellationToken: cancellationToken);

            return default;
        }
    }
}