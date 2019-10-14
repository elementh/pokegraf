using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Core.Responses.PhotoWithKeyboard.Edit;
using Pokegraf.Application.Implementation.Core.Responses.Text;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Pokegraf.Infrastructure.Contract.Service;

namespace Pokegraf.Application.Implementation.Core.Actions.Callbacks.PokemonNext
{
    public class PokemonNextCallbackActionHandler : Pokegraf.Common.Request.CommonHandler<PokemonNextCallbackAction, Result>
    {
        private readonly IPokemonService _pokemonService;

        public PokemonNextCallbackActionHandler(ILogger<Pokegraf.Common.Request.CommonHandler<PokemonNextCallbackAction, Result>> logger,
            IMediator mediatR, IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override async Task<Result> Handle(PokemonNextCallbackAction request, CancellationToken cancellationToken)
        {
            if (!request.Data.ContainsKey("requested_pokemon")) return Result.Success();

            var requestedPokemon = int.Parse(request.Data["requested_pokemon"]);

            var result = await _pokemonService.GetPokemon(requestedPokemon);

            if (!result.Succeeded)
            {
                if (result.Errors.ContainsKey("not_found"))
                {
                    return await MediatR.Send(new TextResponse(result.Errors["not_found"].First() ?? "Ups, there was an error! Try again later!"));
                }

                return result;
            }

            var keyboard = result.Value.ToDescriptionKeyboard();

            return await MediatR.Send(new EditPhotoWithCaptionWithKeyboardResponse(result.Value.Image.ToString(), result.Value.Description, keyboard, request.MessageId));
        }
    }
}