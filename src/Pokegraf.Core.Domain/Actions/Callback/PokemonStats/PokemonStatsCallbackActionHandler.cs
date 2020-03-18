using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Pokegraf.Core.Domain.Extensions;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Core.Domain.Actions.Callback.PokemonStats
{
    public class PokemonStatsCallbackActionHandler : ActionHandler<PokemonStatsCallbackAction>
    {
        protected readonly IPokemonService PokemonService;

        public PokemonStatsCallbackActionHandler(INavigatorContext ctx, IPokemonService pokemonService) : base(ctx)
        {
            PokemonService = pokemonService;
        }

        public override async Task<Unit> Handle(PokemonStatsCallbackAction request, CancellationToken cancellationToken)
        {
            if (!request.ParsedData.ContainsKey("requested_pokemon")) return default;

            var pokemonDto = await PokemonService.GetPokemon(int.Parse(request.ParsedData["requested_pokemon"]));

            if (pokemonDto == null) return default;

            var photo = new InputMediaPhoto(pokemonDto.Image);

            if (string.IsNullOrWhiteSpace(Ctx.GetCallbackQuery().InlineMessageId))
            {
                await Ctx.Client.EditMessageMediaAsync(Ctx.GetTelegramChat(), Ctx.GetCallbackQuery().Message.MessageId, photo,
                    pokemonDto.ToStatsKeyboard(), cancellationToken);

                await Ctx.Client.EditMessageCaptionAsync(Ctx.GetTelegramChat(), Ctx.GetCallbackQuery().Message.MessageId, pokemonDto.ToStatsCaption(),
                    pokemonDto.ToStatsKeyboard(), cancellationToken);
            }
            else
            {
                await Ctx.Client.EditMessageMediaAsync(Ctx.GetCallbackQuery().InlineMessageId, photo,
                    pokemonDto.ToStatsKeyboard(), cancellationToken);

                await Ctx.Client.EditMessageCaptionAsync(Ctx.GetCallbackQuery().InlineMessageId, pokemonDto.ToStatsCaption(),
                    pokemonDto.ToStatsKeyboard(), cancellationToken);
            }

            return default;
        }
    }
}