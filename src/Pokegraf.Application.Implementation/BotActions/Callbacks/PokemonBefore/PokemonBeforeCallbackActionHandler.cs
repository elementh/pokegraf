using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Contract.Client;
using Pokegraf.Application.Implementation.BotActions.Responses.Text;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Service;

namespace Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonBefore
{
    public class PokemonBeforeCallbackActionHandler : Pokegraf.Common.Request.RequestHandler<PokemonBeforeCallbackAction, Result>
    {
        private readonly IPokemonService _pokemonService;
        private readonly IBotClient _bot;

        public PokemonBeforeCallbackActionHandler(
            ILogger<Pokegraf.Common.Request.RequestHandler<PokemonBeforeCallbackAction, Result>> logger, IMediator mediatR,
            IPokemonService pokemonService, IBotClient bot) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
            _bot = bot;
        }

        public override async Task<Result> Handle(PokemonBeforeCallbackAction request, CancellationToken cancellationToken)
        {
            if (!request.Data.Contains("requested_pokemon")) return Result.Success();

            var pokemonBeforeResult = await _pokemonService.GetPokemon((int) request.Data["requested_pokemon"]);

            if (!pokemonBeforeResult.Succeeded)
            {
                if (pokemonBeforeResult.Errors.ContainsKey("not_found"))
                {
                    return await MediatR.Send(new TextResponse(request.Chat.Id,
                        pokemonBeforeResult.Errors["not_found"].First() ?? "Ups, there was an error! Try again later!"));
                }

                return pokemonBeforeResult;
            }

            return Result.Success();
        }
    }
}