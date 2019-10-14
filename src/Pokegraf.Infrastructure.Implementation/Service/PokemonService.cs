using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OperationResult;
using PokeAPI;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Common.Helper;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Pokegraf.Infrastructure.Contract.Service;
using Pokegraf.Infrastructure.Implementation.Mapping.Extension;
using System;
using System.Linq;
using System.Threading.Tasks;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Infrastructure.Implementation.Service
{
    public class PokemonService : IPokemonService
    {
        protected readonly ILogger<PokemonSpecies> Logger;
        protected readonly IDistributedCache Cache;

        public PokemonService(ILogger<PokemonSpecies> logger, IDistributedCache cache)
        {
            Logger = logger;
            Cache = cache;
        }

        public async Task<Result<PokemonDto, Error>> GetPokemon(int pokeNumber)
        {
            var rawCachedPokemon = await Cache.GetStringAsync($"pokemon:{pokeNumber}");

            if (rawCachedPokemon != null)
            {
                Logger.LogTrace("Found pokemon @PokemonId in cache.", pokeNumber);
                
                var cachedPokemon = JsonConvert.DeserializeObject<PokemonDto>(rawCachedPokemon);
                
                return Ok(cachedPokemon);
            }
            
            Pokemon pokemon;
            PokemonSpecies species;
            try
            {
                pokemon = await DataFetcher.GetApiObject<Pokemon>(pokeNumber);
                species = await DataFetcher.GetApiObject<PokemonSpecies>(pokeNumber);
            }
            catch (Exception e)
            {
                if (e.Message == "Response status code does not indicate success: 404 (Not Found).")
                {
                    return Error(NotFound($"The requested pokemon ({pokeNumber}) does not exist."));
                }
                
                Logger.LogError(e, "Unhandled error getting pokemon number {PokeNumber}", pokeNumber);
                
                return Error(UnknownError($"Unhandled error getting pokemon number {pokeNumber}"));
            }
            
            var dto = new PokemonDto()
            {
                Id = pokeNumber,
                Name = GetName(pokemon),
                Description = GetDescription(species),
                Stats = await GetStats(pokeNumber),
                Image = await GetImageUri(pokeNumber),
                Sprite = pokemon.Sprites.FrontMale,
                Before = await GetPokemonBefore(pokeNumber),
                Next = await GetPokemonNext(pokeNumber)
            };

            await Cache.SetStringAsync($"pokemon:{dto.Id}", JsonConvert.SerializeObject(dto));
            
            return Ok(dto);
        }

        public async Task<Result<PokemonDto, Error>> GetPokemon(string pokeName)
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
                    return Error(NotFound($"The requested pokemon ({pokeName}) does not exist."));
                }
                
                Logger.LogError(e, "Unhandled error getting pokemon named {PokeName}", pokeName);
                
                return Error(UnknownError($"Unhandled error getting pokemon number {pokeName}"));
            }

            return await GetPokemon(pokemon.ID);
        }

        public Result<PokemonFusionDto, Error> GetFusion()
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

        public async Task<Result<BerryDto, Error>> GetBerry(string berryName)
        {
            var rawCachedBerry = await Cache.GetStringAsync($"berry:{berryName}");

            if (rawCachedBerry != null)
            {
                Logger.LogTrace("Found berry @BerryName in cache.", berryName);

                var cachedBerry = JsonConvert.DeserializeObject<BerryDto>(rawCachedBerry);

                return Ok(cachedBerry);
            }

            Berry berry;
            PokemonType type;
            
            try
            {
                berry = await DataFetcher.GetNamedApiObject<Berry>(berryName);
                //TODO: move type to another method, cache it.
                type = await DataFetcher.GetNamedApiObject<PokemonType>(berry.NaturalGiftType.Name);
            }
            catch (Exception e)
            {
                if (e.Message == "Response status code does not indicate success: 404 (Not Found).")
                {
                    return Error(NotFound($"The requested berry ({berryName}) does not exist."));
                }
                
                Logger.LogError(e, "Unhandled error getting berry {BerryName}", berryName);
                
                return Error(UnknownError($"Unhandled error getting berry {berryName}"));
            }

            var dto = berry.ToBerryDto(type);
            
            await Cache.SetStringAsync($"berry:{dto.Name.ToLower()}", JsonConvert.SerializeObject(dto));

            return Ok(dto);
        }

        protected string GetDescription(PokemonSpecies species)
        {
            return species.FlavorTexts.First(text => text.Language.Name == "en").FlavorText.Replace("\n", " ");
        }

        protected string GetName(Pokemon pokemon)
        {
            var name = pokemon.Name.FirstLetterToUpperCase();

            if (name.Contains('-'))
            {
                name = name.Substring(0, name.IndexOf('-'));
            }

            return name;
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
        
        protected async Task<string> GetImageUri(int pokeNumber)
        {
            if (pokeNumber > 721)
            {
                var pokemon = await DataFetcher.GetApiObject<Pokemon>(pokeNumber);

                return $"https://img.pokemondb.net/artwork/large/{pokemon.Name}.jpg";

            }

            return $"https://veekun.com/dex/media/pokemon/global-link/{pokeNumber}.png";
        }

        protected async Task<Tuple<int, string>> GetPokemonNext(int pokeNumber)
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
        
        protected async Task<Tuple<int, string>> GetPokemonBefore(int pokeNumber)
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
    }
}