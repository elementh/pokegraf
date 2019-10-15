using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Implementation.Core.Responses.Inline;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.Stats.Command.AddOneToPokemonRequests;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.InlineQueryResults;

namespace Pokegraf.Application.Implementation.Core.Actions.Inline.Pokemon
{
    public class PokemonInlineActionHandler : IRequestHandler<PokemonInlineAction, Status<Error>>
    {
        protected readonly ILogger<PokemonInlineActionHandler> Logger;
        protected readonly IMediator Mediator;
        protected readonly IPokemonService PokemonService;

        public PokemonInlineActionHandler(ILogger<PokemonInlineActionHandler> logger, IMediator mediator, IPokemonService pokemonService)
        {
            Logger = logger;
            Mediator = mediator;
            PokemonService = pokemonService;
        }

        public async Task<Status<Error>> Handle(PokemonInlineAction request, CancellationToken cancellationToken)
        {
            var requestedPokemon = request.Query.ToLower();

            var result = int.TryParse(requestedPokemon, out var pokeNumber)
                ? await PokemonService.GetPokemon(pokeNumber)
                : await PokemonService.GetPokemon(requestedPokemon);
            
            if (result.IsError) return await Mediator.Send(new InlineResponse(new InlineQueryResultBase[]{}), cancellationToken);

            var sprite = result.Value.Sprite ?? result.Value.Image;
            
            InlineQueryResultBase[] results =
            {
                new InlineQueryResultPhoto($"pokemon:{result.Value.Id}", result.Value.Image, sprite)
                {
                    Caption = $"{result.Value.Description}"
                }
            };
            
            await Mediator.Send(new AddOneToPokemonRequestsCommand {UserId = request.From.Id}, cancellationToken);

            return await Mediator.Send(new InlineResponse(results), cancellationToken);
        }
    }
}