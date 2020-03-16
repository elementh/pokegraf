using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Pokegraf.Common.Helper;
using Pokegraf.Core.Domain.Extensions;
using Pokegraf.Common.Resources;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Core.Domain.Actions.Command.Pokemon
{
    public class PokemonCommandActionHandler : ActionHandler<PokemonCommandAction>
    {
        protected readonly IPokemonService PokemonService;

        public PokemonCommandActionHandler(INavigatorContext ctx, IPokemonService pokemonService) : base(ctx)
        {
            PokemonService = pokemonService;
        }

        public override async Task<Unit> Handle(PokemonCommandAction request, CancellationToken cancellationToken)
        {
            var commandArgs = Ctx.Update.Message.Text?.Split(" ");

            var requestedPokemon = commandArgs != null && commandArgs.Length > 1
                ? commandArgs[1]
                : RandomProvider.GetThreadRandom().Next(1, 721).ToString();

            var pokemon = int.TryParse(requestedPokemon, out var pokeNumber)
                ? await PokemonService.GetPokemon(pokeNumber)
                : await PokemonService.GetPokemon(requestedPokemon);

            if (pokemon == null)
            {
                await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), PokegrafResources.DefaultErrorMessage, ParseMode.Markdown,
                    cancellationToken: cancellationToken);

                return default;
            }

            await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), pokemon.Image, pokemon.Description, ParseMode.Markdown,
                replyMarkup: pokemon.ToDescriptionKeyboard(), cancellationToken: cancellationToken);

            return default;
        }
    }
}