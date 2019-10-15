using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Responses.Photo.WithKeyboard;
using Pokegraf.Application.Contract.Core.Responses.Text;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Common.Helper;
using Pokegraf.Infrastructure.Contract.Service;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Core.Actions.Command.Pokemon
{
    public class PokemonCommandActionHandler : IRequestHandler<PokemonCommandAction, Status<Error>>
    {
        protected readonly ILogger<PokemonCommandActionHandler> Logger;
        protected readonly IMediator Mediator;
        protected readonly IPokemonService PokemonService;

        public PokemonCommandActionHandler(ILogger<PokemonCommandActionHandler> logger, IMediator mediator, IPokemonService pokemonService)
        {
            Logger = logger;
            Mediator = mediator;
            PokemonService = pokemonService;
        }
        
        public async Task<Status<Error>> Handle(PokemonCommandAction request, CancellationToken cancellationToken)
        {
            var commandArgs = request.Text?.Split(" ");

            string requestedPokemon;
            
            if (commandArgs != null && commandArgs.Length > 1)
            {
                requestedPokemon = commandArgs[1];
            }
            else
            {
                requestedPokemon =  RandomProvider.GetThreadRandom().Next(1, 721).ToString();
            }

            var result = int.TryParse(requestedPokemon, out var pokeNumber)
                ? await PokemonService.GetPokemon(pokeNumber)
                : await PokemonService.GetPokemon(requestedPokemon);

            if (result.IsError)
            {
                if (result.Error.Type == ErrorType.NotFound)
                {
                    return await Mediator.Send(new TextResponse(result.Error.Message ?? "Ups, there was an error! Try again later!"));
                }

                return Error(result.Error);
            }

            var keyboard = result.Value.ToDescriptionKeyboard();

            return await Mediator.Send(new PhotoWithKeyboardResponse(result.Value.Image.ToString(), $"{result.Value.Description}", keyboard));
        }
    }
}