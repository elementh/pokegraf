using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Event;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Dto;
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
            var requestedPokemon = request.Text?.Split(" ")?[1];

            if (string.IsNullOrWhiteSpace(requestedPokemon)) Result.Success();

            Result<PokemonDto> result;

            result = int.TryParse(requestedPokemon, out var pokeNumber)
                ? await _pokemonService.GetPokemon(pokeNumber)
                : await _pokemonService.GetPokemon(requestedPokemon);

            if (result.Succeeded)
            {
                await MediatR.Publish(new PhotoResponseRequest
                {
                    ChatId = request.Chat.Id,
                    Photo = result.Value.Image,
                    Caption = $"{result.Value.Name}: {result.Value.Description}"
                });
            }
            else if (result.Errors.ContainsKey("not_found"))
            {
                await MediatR.Publish(new TextResponseRequest
                {
                    ChatId = request.Chat.Id,
                    Text = result.Errors["not_found"].First() ?? "Ups, there was an error! Try again later!"
                });
            }
            
            return Result.Success();
        }
    }
}