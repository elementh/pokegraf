using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Application.Implementation.BotActions.Responses.PhotoWithKeyboard.Edit;
using Pokegraf.Application.Implementation.BotActions.Responses.Text;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Dto;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonBefore
{
    public class PokemonBeforeCallbackActionHandler : Pokegraf.Common.Request.RequestHandler<PokemonBeforeCallbackAction, Result>
    {
        private readonly IPokemonService _pokemonService;

        public PokemonBeforeCallbackActionHandler(
            ILogger<Pokegraf.Common.Request.RequestHandler<PokemonBeforeCallbackAction, Result>> logger, IMediator mediatR,
            IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override async Task<Result> Handle(PokemonBeforeCallbackAction request, CancellationToken cancellationToken)
        {
            if (!request.Data.ContainsKey("requested_pokemon")) return Result.Success();

            var requestedPokemon = int.Parse(request.Data["requested_pokemon"]);
            
            var result = await _pokemonService.GetPokemon(requestedPokemon);

            if (!result.Succeeded)
            {
                if (result.Errors.ContainsKey("not_found"))
                {
                    return await MediatR.Send(new TextResponse(request.Chat.Id,
                        result.Errors["not_found"].First() ?? "Ups, there was an error! Try again later!"));
                }

                return result;
            }
            
            var keyboard = result.Value.ToDescriptionKeyboard();

            return await MediatR.Send(new EditPhotoWithCaptionWithKeyboardResponse(request.Chat.Id, result.Value.Image.ToString(),
                result.Value.Description, keyboard, request.MessageId));
        }
    }
}