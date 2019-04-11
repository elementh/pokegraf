using System.Collections.Generic;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Pokegraf.Infrastructure.Contract.Dto;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.Mapping.Extension
{
    public static class PokemonDtoExtension
    {
        public static InlineKeyboardMarkup ToDescriptionKeyboard(this PokemonDto pokemonDto)
        {
            var pokemonBeforeCallback = new Dictionary<string, string>
            {
                {"action", "pokemon_before"}, 
                {"requested_pokemon", pokemonDto.Before.Item1.ToString()}
            };
            
            var pokemonNextCallback = new Dictionary<string, string>
            {
                {"action", "pokemon_next"},
                {"requested_pokemon", pokemonDto.Next.Item1.ToString()}
            };

            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData($"Show stats", "no_callback"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData($"⬅ {pokemonDto.Before.Item2}", JsonConvert.SerializeObject(pokemonBeforeCallback)),
                    InlineKeyboardButton.WithCallbackData($"{pokemonDto.Name}", "no_callback"),
                    InlineKeyboardButton.WithCallbackData($"{pokemonDto.Next.Item2} ➡", JsonConvert.SerializeObject(pokemonNextCallback))
                }
            });
        }
    }
}