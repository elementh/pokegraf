using System.Collections.Generic;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonStats;
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
            
            var pokemonStatsCallback = new Dictionary<string, string>
            {
                {"action", "pokemon_stats"},
                {"requested_pokemon", pokemonDto.Id.ToString()}
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
                    InlineKeyboardButton.WithCallbackData($"{pokemonDto.Name}", JsonConvert.SerializeObject(pokemonStatsCallback)),
                    InlineKeyboardButton.WithCallbackData($"{pokemonDto.Next.Item2} ➡", JsonConvert.SerializeObject(pokemonNextCallback))
                }
            });
        }
        
        public static InlineKeyboardMarkup ToStatsKeyboard(this PokemonDto pokemonDto)
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
            
            var pokemonDescriptionCallback = new Dictionary<string, string>
            {
                {"action", "pokemon_description"},
                {"requested_pokemon", pokemonDto.Id.ToString()}
            };

            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData($"Show description", "no_callback"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData($"⬅ {pokemonDto.Before.Item2}", JsonConvert.SerializeObject(pokemonBeforeCallback)),
                    InlineKeyboardButton.WithCallbackData($"{pokemonDto.Name}", JsonConvert.SerializeObject(pokemonDescriptionCallback)),
                    InlineKeyboardButton.WithCallbackData($"{pokemonDto.Next.Item2} ➡", JsonConvert.SerializeObject(pokemonNextCallback))
                }
            });
        }
    }
}