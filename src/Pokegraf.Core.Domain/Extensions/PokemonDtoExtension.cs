using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Core.Domain.Extensions
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
                    InlineKeyboardButton.WithCallbackData($"Show stats", JsonConvert.SerializeObject(pokemonStatsCallback))
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData($"⬅ {pokemonDto.Before.Item2}", JsonConvert.SerializeObject(pokemonBeforeCallback)),
                    InlineKeyboardButton.WithCallbackData($"{pokemonDto.Name}", "no_callback"),
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
                    InlineKeyboardButton.WithCallbackData($"Show description", JsonConvert.SerializeObject(pokemonDescriptionCallback))
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData($"⬅ {pokemonDto.Before.Item2}", JsonConvert.SerializeObject(pokemonBeforeCallback)),
                    InlineKeyboardButton.WithCallbackData($"{pokemonDto.Name}", "no_callback"),
                    InlineKeyboardButton.WithCallbackData($"{pokemonDto.Next.Item2} ➡", JsonConvert.SerializeObject(pokemonNextCallback))
                }
            });
        }

        public static string ToStatsCaption(this PokemonDto pokemonDto)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"HP 💗 {pokemonDto.Stats.Health}");
            sb.AppendLine($"Attack 💥 {pokemonDto.Stats.Attack}");
            sb.AppendLine($"Defense 🛡 {pokemonDto.Stats.Defense}");
            sb.AppendLine($"Special Attack 🌟 {pokemonDto.Stats.SpecialAttack}");
            sb.AppendLine($"Special Defense 🔰 {pokemonDto.Stats.SpecialDefense}");
            sb.AppendLine($"Speed 👟 {pokemonDto.Stats.Speed}");

            return sb.ToString();
        }
    }
}