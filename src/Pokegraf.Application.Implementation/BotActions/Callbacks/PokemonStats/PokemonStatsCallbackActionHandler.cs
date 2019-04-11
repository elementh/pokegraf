using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.BotActions.Responses.PhotoWithKeyboard.Edit;
using Pokegraf.Application.Implementation.BotActions.Responses.Text;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Service;

namespace Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonStats
{
    public class PokemonStatsCallbackActionHandler : Pokegraf.Common.Request.RequestHandler<PokemonStatsCallbackAction, Result>
    {
        private readonly IPokemonService _pokemonService;

        public PokemonStatsCallbackActionHandler(ILogger<Pokegraf.Common.Request.RequestHandler<PokemonStatsCallbackAction, Result>> logger,
            IMediator mediatR, IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override async Task<Result> Handle(PokemonStatsCallbackAction request, CancellationToken cancellationToken)
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

            var keyboard = result.Value.ToStatsKeyboard();

            var sb = new StringBuilder();
            
            sb.AppendLine($"HP 💗 {result.Value.Stats.Health}");
            sb.AppendLine($"Attack 💥 {result.Value.Stats.Attack}");
            sb.AppendLine($"Defense 🛡 {result.Value.Stats.Defense}");
            sb.AppendLine($"Special Attack 🌟 {result.Value.Stats.SpecialAttack}");
            sb.AppendLine($"Special Defense 🔰 {result.Value.Stats.SpecialDefense}");
            sb.AppendLine($"Speed 👟 {result.Value.Stats.Speed}");

            var statsTable = sb.ToString();

            return await MediatR.Send(new EditPhotoWithCaptionWithKeyboardResponse(request.Chat.Id, result.Value.Image.ToString(),
                statsTable, keyboard, request.MessageId));
        }
    }
}