using System;
using System.Linq;
using System.Threading.Tasks;
using OperationResult;
using PokeAPI;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Common.Helper;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using static OperationResult.Helpers;

namespace Pokegraf.Infrastructure.Implementation.Helper
{
    public static class PokeHelper
    {
        public static string GetDescription(PokemonSpecies species)
        {
            return species.FlavorTexts.First(text => text.Language.Name == "en").FlavorText.Replace("\n", " ");
        }

        public static string GetName(Pokemon pokemon)
        {
            var name = pokemon.Name.FirstLetterToUpperCase();

            if (name.Contains('-'))
            {
                name = name.Substring(0, name.IndexOf('-'));
            }

            return name;
        }
        
        public static async Task<PokemonDto.StatsDto> GetStats(int pokeNumber)
        {
            var pokemon = await DataFetcher.GetApiObject<Pokemon>(pokeNumber);
            return new PokemonDto.StatsDto
            {
                Health = pokemon.Stats[5].BaseValue,
                Attack = pokemon.Stats[4].BaseValue,
                Defense = pokemon.Stats[4].BaseValue,
                SpecialAttack = pokemon.Stats[2].BaseValue,
                SpecialDefense = pokemon.Stats[1].BaseValue,
                Speed = pokemon.Stats[0].BaseValue
            };
        }
        
        public static async Task<string> GetImageUri(int pokeNumber)
        {
            if (pokeNumber > 721)
            {
                var pokemon = await DataFetcher.GetApiObject<Pokemon>(pokeNumber);

                return $"https://img.pokemondb.net/artwork/large/{pokemon.Name}.jpg";

            }

            return $"https://veekun.com/dex/media/pokemon/global-link/{pokeNumber}.png";
        }

        public static async Task<Tuple<int, string>> GetPokemonNext(int pokeNumber)
        {
            switch (pokeNumber)
            {
                case 807:
                    pokeNumber = 1;
                    break;
                default:
                    ++pokeNumber;
                    break;
            }

            var pokemon = await DataFetcher.GetApiObject<Pokemon>(pokeNumber);
            
            return new Tuple<int, string>(pokeNumber, pokemon.Name.FirstLetterToUpperCase());
        }
        
        public static async Task<Tuple<int, string>> GetPokemonBefore(int pokeNumber)
        {
            switch (pokeNumber)
            {
                case 1:
                    pokeNumber = 807;
                    break;
                default:
                    --pokeNumber;
                    break;
            }

            var pokemon = await DataFetcher.GetApiObject<Pokemon>(pokeNumber);
            
            return new Tuple<int, string>(pokeNumber, pokemon.Name.FirstLetterToUpperCase());
        }
        
        public static Result<PokemonFusionDto, Error> GetFusion()
        {
            string[] firstHalf = {"Bulb", "Ivy", "Venu", "Char", "Char", "Char", "Squirt", "War", "Blast", "Cater", "Meta", "Butter", 
                "Wee", "Kak", "Bee", "Pid", "Pidg", "Pidg", "Rat", "Rat", "Spear", "Fear", "Ek", "Arb", "Pika", "Rai", "Sand", "Sand", "Nido", 
                "Nido", "Nido", "Nido", "Nido", "Nido", "Clef", "Clef", "Vul", "Nine", "Jiggly", "Wiggly", "Zu", "Gol", "Odd", "Gloo", "Vile", 
                "Pa", "Para", "Veno", "Veno", "Dig", "Dug", "Meow", "Per", "Psy", "Gol", "Man", "Prime", "Grow", "Arca", "Poli", "Poli", "Poli", 
                "Ab", "Kada", "Ala", "Ma", "Ma", "Ma", "Bell", "Weepin", "Victree", "Tenta", "Tenta", "Geo", "Grav", "Gol", "Pony", "Rapi", 
                "Slow", "Slow", "Magne", "Magne", "Far", "Do", "Do", "See", "Dew", "Gri", "Mu", "Shell", "Cloy", "Gas", "Haunt", "Gen", "On", 
                "Drow", "Hyp", "Krab", "King", "Volt", "Electr", "Exegg", "Exegg", "Cu", "Maro", "Hitmon", "Hitmon", "Licki", "Koff", "Wee", 
                "Rhy", "Rhy", "Chan", "Tang", "Kangas", "Hors", "Sea", "Gold", "Sea", "Star", "Star", "Mr.", "Scy", "Jyn", "Electa", "Mag", "Pin",
                "Tau", "Magi", "Gyara", "Lap", "Dit", "Ee", "Vapor", "Jolt", "Flare", "Pory", "Oma", "Oma", "Kabu", "Kabu", "Aero", "Snor", 
                "Artic", "Zap", "Molt", "Dra", "Dragon", "Dragon", "Mew", "Mew"};
            
            string[] secondHalf = {"basaur", "ysaur", "usaur", "mander", "meleon", "izard", "tle", "tortle", "toise", "pie", "pod",
                "free", "dle", "una", "drill", "gey", "eotto", "eot", "tata", "icate", "row", "row", "kans", "bok", "chu", "chu", "shrew",
                "slash", "oran", "rina", "queen", "ran", "rino", "king", "fairy", "fable", "pix", "tales", "puff", "tuff", "bat", "bat", "ish",
                "oom", "plume", "ras", "sect", "nat", "moth", "lett", "trio", "th", "sian", "duck", "duck", "key", "ape", "lithe", "nine", "wag",
                "whirl", "wrath", "ra", "bra", "kazam", "chop", "choke", "champ", "sprout", "bell", "bell", "cool", "cruel", "dude", "eler", "em", 
                "ta", "dash", "poke", "bro", "mite", "ton", "fetchd", "duo", "drio", "eel", "gong", "mer", "uk", "der", "ster", "tly", "ter",
                "gar", "ix", "zee", "no", "by", "ler", "orb", "ode", "cute", "utor", "bone", "wak", "lee", "chan", "tung", "fing", "zing", "horn",
                "don", "sey", "gela", "khan", "sea", "dra", "deen", "king", "yu", "mie", "mime", "ther", "nx", "buzz", "mar", "sir", "ros", 
                "karp", "dos", "ras", "to", "vee", "eon", "eon", "eon", "gon", "nyte", "star", "to", "tops", "dactyl", "lax", "cuno", "dos", 
                "tres", "tini", "nair", "nite", "two", "ew"};
            
            var firstPokemon =  RandomProvider.GetThreadRandom().Next(1, 151);
            var secondPokemon =  RandomProvider.GetThreadRandom().Next(1, 151);
            
            while (firstPokemon == secondPokemon) {
                secondPokemon =  RandomProvider.GetThreadRandom().Next(1, 151);
            }

            var image = new Uri($"http://images.alexonsager.net/pokemon/fused/{firstPokemon}/{firstPokemon}.{secondPokemon}.png");
            var name = $"{firstHalf[secondPokemon - 1]}{secondHalf[firstPokemon - 1]}";
            
            var pokemonFusionDto = new PokemonFusionDto(name, image);
            
            return Ok(pokemonFusionDto);
        }
    }
}