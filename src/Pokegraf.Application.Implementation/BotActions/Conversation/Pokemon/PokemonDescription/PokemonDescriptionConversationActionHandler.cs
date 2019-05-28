using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Common.Responses.PhotoWithKeyboard.Send;
using Pokegraf.Application.Implementation.Common.Responses.Text;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Pokegraf.Infrastructure.Contract.Service;

namespace Pokegraf.Application.Implementation.BotActions.Conversation.Pokemon.PokemonDescription
{
    public class PokemonDescriptionConversationActionHandler : Pokegraf.Common.Request.RequestHandler<PokemonDescriptionConversationAction, Result>
    {
        private readonly IPokemonService _pokemonService;

        public PokemonDescriptionConversationActionHandler(ILogger<Pokegraf.Common.Request.RequestHandler<PokemonDescriptionConversationAction, Result>> logger, IMediator mediatR, IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override async Task<Result> Handle(PokemonDescriptionConversationAction request, CancellationToken cancellationToken)
        {
            Result<PokemonDto> pokemonResult;

            if (request.Parameters.TryGetValue("requested_pokemon_name", out var requestedPokemonName) && !string.IsNullOrWhiteSpace(requestedPokemonName))
            {
                pokemonResult = await _pokemonService.GetPokemon(requestedPokemonName.ToLower());
            } 
            else if (request.Parameters.TryGetValue("requested_pokemon_number", out var requestedPokemonNumber) && !string.IsNullOrWhiteSpace(requestedPokemonNumber))
            {
                int.TryParse(requestedPokemonNumber, out var pokeNumber);
                
                pokemonResult = await _pokemonService.GetPokemon(pokeNumber);
            }
            else
            {
                pokemonResult = await _pokemonService.GetPokemon(new Random().Next(1, 721));
            }
            
            if (!pokemonResult.Succeeded)
            {
                if (pokemonResult.Errors.ContainsKey("not_found"))
                {
                    return await MediatR.Send(new TextResponse("Sorry, I couldn't find that pokemon, why don't you try again?"));
                }

                return pokemonResult;
            }
            
            var keyboard = pokemonResult.Value.ToDescriptionKeyboard();

            return await MediatR.Send(new PhotoWithCaptionWithKeyboardResponse(pokemonResult.Value.Image.ToString(), $"{pokemonResult.Value.Description}", keyboard));
        }
    }
}