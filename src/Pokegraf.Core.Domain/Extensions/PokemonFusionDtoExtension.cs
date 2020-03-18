using Pokegraf.Common.Resources;
using Pokegraf.Core.Domain.Resources;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Core.Domain.Extensions
{
    public static class PokemonFusionDtoExtension
    {
        public static InlineQueryResultPhoto ToInlineQueryResultPhoto(this PokemonFusionDto pokemonFusionDto)
        {
            return new InlineQueryResultPhoto(
                $"pokemon:{pokemonFusionDto.Name.ToLower()}",
                pokemonFusionDto.Image.ToString(),
                pokemonFusionDto.Image.ToString())
            {
                Caption = pokemonFusionDto.Name,
                Title = pokemonFusionDto.Name,
                Description = "Fusion!",
                ReplyMarkup = FusionKeyboard
            };
        }

        public static InlineKeyboardMarkup FusionKeyboard => new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData(
            PokegrafResources.MoreFusion, CallbackActions.FusionAction));
    }
}