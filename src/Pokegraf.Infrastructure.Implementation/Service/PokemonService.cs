using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PokeAPI;
using Pokegraf.Common.Helper;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Dto;
using Pokegraf.Infrastructure.Contract.Service;

namespace Pokegraf.Infrastructure.Implementation.Service
{
    public class PokemonService : IPokemonService
    {
        protected readonly ILogger<PokemonSpecies> Logger;

        public PokemonService(ILogger<PokemonSpecies> logger)
        {
            Logger = logger;
        }

        public async Task<Result<PokemonDto>> GetPokemon(int pokeNumber)
        {
            Pokemon pokemon;
            
            try
            {
                pokemon = await DataFetcher.GetApiObject<Pokemon>(pokeNumber);
            }
            catch (Exception e)
            {
                if (e.Message == "Response status code does not indicate success: 404 (Not Found).")
                {
                    return Result<PokemonDto>.NotFound(new List<string> {"The requested pokemon does not exist."});
                }
                
                Logger.LogError(e, "Unhandled error getting pokemon number {PokeNumber}", pokeNumber);
                
                return Result<PokemonDto>.UnknownError(new List<string> {$"Unhandled error getting pokemon number {pokeNumber}", e.Message});
            }
            
            var dto = new PokemonDto()
            {
                Id = pokeNumber,
                Name = pokemon.Name.FirstLetterToUpperCase(),
                Description = await GetDescription(pokeNumber),
                Stats = await GetStats(pokeNumber),
                Image = GetImageUri(pokeNumber),
                Sprite = new Uri(pokemon.Sprites.FrontMale),
                Before = await GetPokemonBefore(pokeNumber),
                Next = await GetPokemonNext(pokeNumber)
            };
            
            return Result<PokemonDto>.Success(dto);
        }

        public async Task<Result<PokemonDto>> GetPokemon(string pokeName)
        {
            Pokemon pokemon;
            
            try
            {
                pokemon = await DataFetcher.GetNamedApiObject<Pokemon>(pokeName);
            }
            catch (Exception e)
            {
                if (e.Message == "Response status code does not indicate success: 404 (Not Found).")
                {
                    return Result<PokemonDto>.NotFound(new List<string> {"The requested pokemon does not exist."});
                }
                
                Logger.LogError(e, "Unhandled error getting pokemon named {PokeName}", pokeName);
                
                return Result<PokemonDto>.UnknownError(new List<string> {$"Unhandled error getting pokemon number {pokeName}", e.Message});
            }

            return await GetPokemon(pokemon.ID);
        }

        public Result<Tuple<string, Uri>> GetFusion()
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
            
            var firstPokemon = new Random().Next(1, 151);
            var secondPokemon = new Random().Next(1, 151);
            
            while (firstPokemon == secondPokemon) {
                secondPokemon = new Random().Next(1, 151);
            }

            Uri photoUrl = new Uri($"http://images.alexonsager.net/pokemon/fused/{firstPokemon}/{firstPokemon}.{secondPokemon}.png");
            var caption = $"{firstHalf[secondPokemon - 1]}{secondHalf[firstPokemon - 1]}";
            
            var tuple = new Tuple<string, Uri>(caption, photoUrl);
            
            return Result<Tuple<string, Uri>>.Success(tuple);
        }

        protected async Task<string> GetDescription(int pokeNumber)
        {
            var species = await DataFetcher.GetApiObject<PokemonSpecies>(pokeNumber);

            return species.FlavorTexts.First(text => text.Language.Name == "en").FlavorText.Replace("\n", " ");
        }

        protected async Task<PokemonDto.StatsDto> GetStats(int pokeNumber)
        {
            var pokemon = await DataFetcher.GetApiObject<Pokemon>(pokeNumber);
            return new PokemonDto.StatsDto
            {
                Health = pokemon.Stats[5].BaseValue,
                Attack = pokemon.Stats[4].BaseValue,
                Defense = pokemon.Stats[4].BaseValue,
                SpecialAttack = pokemon.Stats[2].BaseValue,
                SpecialDefense = pokemon.Stats[1].BaseValue,
                Speed = pokemon.Stats[0].BaseValue,
            };
        }
        
        protected Uri GetImageUri(int pokeNumber)
        {
            return new Uri($"https://veekun.com/dex/media/pokemon/global-link/{pokeNumber}.png");
        }

        protected async Task<Tuple<int, string>> GetPokemonNext(int pokeNumber)
        {
            switch (pokeNumber)
            {
                case 721:
                    pokeNumber = 1;
                    break;
                default:
                    ++pokeNumber;
                    break;
            }

            var pokemon = await DataFetcher.GetApiObject<Pokemon>(pokeNumber);
            
            return new Tuple<int, string>(pokeNumber, pokemon.Name.FirstLetterToUpperCase());
        }
        
        protected async Task<Tuple<int, string>> GetPokemonBefore(int pokeNumber)
        {
            switch (pokeNumber)
            {
                case 1:
                    pokeNumber = 721;
                    break;
                default:
                    --pokeNumber;
                    break;
            }

            var pokemon = await DataFetcher.GetApiObject<Pokemon>(pokeNumber);
            
            return new Tuple<int, string>(pokeNumber, pokemon.Name.FirstLetterToUpperCase());
        }
    }
}