using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Implementation.Core.Responses.Photo.WithKeyboard.Edit;
using Pokegraf.Application.Implementation.Core.Responses.Text;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.Stats.Command.AddOneToPokemonRequests;
using Pokegraf.Infrastructure.Contract.Service;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Core.Actions.Callback.PokemonBefore
{
    public class PokemonBeforeCallbackActionHandler : IRequestHandler<PokemonBeforeCallbackAction, Status<Error>>
    {
        protected readonly ILogger<PokemonBeforeCallbackActionHandler> Logger;
        protected readonly IMediator Mediator;
        protected readonly IPokemonService PokemonService;

        public PokemonBeforeCallbackActionHandler(ILogger<PokemonBeforeCallbackActionHandler> logger, IMediator mediator, IPokemonService pokemonService)
        {
            Logger = logger;
            Mediator = mediator;
            PokemonService = pokemonService;
        }

        public async Task<Status<Error>> Handle(PokemonBeforeCallbackAction request, CancellationToken cancellationToken)
        {
            if (!request.Data.ContainsKey("requested_pokemon")) return Ok();

            var requestedPokemon = int.Parse(request.Data["requested_pokemon"]);
            
            var result = await PokemonService.GetPokemon(requestedPokemon);

            if (result.IsError)
            {
                if (result.Error.Type == ErrorType.NotFound)
                {
                    return await Mediator.Send(new TextResponse(result.Error.Message ?? "Ups, there was an error! Try again later!"));
                }

                return Error(result.Error);
            }
            
            var keyboard = result.Value.ToDescriptionKeyboard();

            return await Mediator.Send(new PhotoWithKeyboardEditResponse(result.Value.Image, result.Value.Description, keyboard, request.MessageId), cancellationToken);
        }
    }
}