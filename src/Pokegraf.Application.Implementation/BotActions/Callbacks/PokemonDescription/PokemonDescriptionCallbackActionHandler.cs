using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Common.Responses.PhotoWithKeyboard.Edit;
using Pokegraf.Application.Implementation.Common.Responses.Text;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Service;

namespace Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonDescription
{
    public class PokemonDescriptionCallbackActionHandler : Pokegraf.Common.Request.RequestHandler<PokemonDescriptionCallbackAction, Result>
    {
        private readonly IPokemonService _pokemonService;

        public PokemonDescriptionCallbackActionHandler(
            ILogger<Pokegraf.Common.Request.RequestHandler<PokemonDescriptionCallbackAction, Result>> logger, IMediator mediatR,
            IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override async Task<Result> Handle(PokemonDescriptionCallbackAction request, CancellationToken cancellationToken)
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

            return await MediatR.Send(new EditPhotoWithCaptionWithKeyboardResponse(result.Value.Image.ToString(),
                result.Value.Description, keyboard, request.MessageId));
        }
    }
}