using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Pokegraf.Core.Domain.Extensions;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types;

namespace Pokegraf.Core.Domain.Actions.Callback.PokemonPrevious
{
    public class PokemonPreviousCallbackActionHandler : ActionHandler<PokemonPreviousCallbackAction>
    {
        protected readonly IPokemonService PokemonService;

        public PokemonPreviousCallbackActionHandler(INavigatorContext ctx, IPokemonService pokemonService) : base(ctx)
        {
            PokemonService = pokemonService;
        }

        public override async Task<Unit> Handle(PokemonPreviousCallbackAction request, CancellationToken cancellationToken)
        {
            if (!request.ParsedData.ContainsKey("requested_pokemon")) return default;

            var pokemonDto = await PokemonService.GetPokemon(int.Parse(request.ParsedData["requested_pokemon"]));

            if (pokemonDto == null) return default;

            var photo = new InputMediaPhoto(pokemonDto.Image);

            if (string.IsNullOrWhiteSpace(Ctx.GetCallbackQuery().InlineMessageId))
            {
                await Ctx.Client.EditMessageMediaAsync(Ctx.GetTelegramChat(), Ctx.GetCallbackQuery().Message.MessageId, photo,
                    pokemonDto.ToDescriptionKeyboard(), cancellationToken);

                await Ctx.Client.EditMessageCaptionAsync(Ctx.GetTelegramChat(), Ctx.GetCallbackQuery().Message.MessageId, pokemonDto.Description,
                    cancellationToken: cancellationToken);
            }
            else
            {
                await Ctx.Client.EditMessageMediaAsync(Ctx.GetCallbackQuery().InlineMessageId, photo,
                    pokemonDto.ToDescriptionKeyboard(), cancellationToken);

                await Ctx.Client.EditMessageCaptionAsync(Ctx.GetCallbackQuery().InlineMessageId, pokemonDto.Description,
                    cancellationToken: cancellationToken);
            }

            return default;
        }
    }
}