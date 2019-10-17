using Pokegraf.Infrastructure.Contract.Dto;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Telegram.Bot.Types.InlineQueryResults;

namespace Pokegraf.Application.Implementation.Mapping.Extension
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
                Caption = pokemonFusionDto.Name
            };
        }
    }
}