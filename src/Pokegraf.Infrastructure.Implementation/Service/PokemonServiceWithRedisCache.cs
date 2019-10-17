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
using Pokegraf.Infrastructure.Implementation.Helper;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Infrastructure.Implementation.Service
{
    public class PokemonServiceWithRedisCache : IPokemonService
    {
        protected readonly ILogger<PokemonSpecies> Logger;
        protected readonly IDistributedCache Cache;

        public PokemonServiceWithRedisCache(ILogger<PokemonSpecies> logger, IDistributedCache cache)
        {
            Logger = logger;
            Cache = cache;
        }

        public async Task<Result<PokemonDto, Error>> GetPokemon(int pokeNumber)
        {
            var rawCachedPokemon = await Cache.GetStringAsync($"pokemon:{pokeNumber}");

            if (rawCachedPokemon != null)
            {
                Logger.LogTrace("Found pokemon {@PokemonId} in cache.", pokeNumber);
                
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
                
                Logger.LogError(e, "Unhandled error getting pokemon number {@PokeNumber}", pokeNumber);
                
                return Error(UnknownError($"Unhandled error getting pokemon number {pokeNumber}"));
            }
            
            var dto = new PokemonDto
            {
                Id = pokeNumber,
                Name = PokeHelper.GetName(pokemon),
                Description = PokeHelper.GetDescription(species),
                Stats = await PokeHelper.GetStats(pokeNumber),
                Image = await PokeHelper.GetImageUri(pokeNumber),
                Sprite = pokemon.Sprites.FrontMale,
                Before = await PokeHelper.GetPokemonBefore(pokeNumber),
                Next = await PokeHelper.GetPokemonNext(pokeNumber)
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
                
                Logger.LogError(e, "Unhandled error getting pokemon named {@PokeName}", pokeName);
                
                return Error(UnknownError($"Unhandled error getting pokemon number {pokeName}"));
            }

            return await GetPokemon(pokemon.ID);
        }

        public Result<PokemonFusionDto, Error> GetFusion()
        {
            return PokeHelper.GetFusion();
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
                
                Logger.LogError(e, "Unhandled error getting berry {@BerryName}", berryName);
                
                return Error(UnknownError($"Unhandled error getting berry {berryName}"));
            }

            var dto = berry.ToBerryDto(type);
            
            await Cache.SetStringAsync($"berry:{dto.Name.ToLower()}", JsonConvert.SerializeObject(dto));

            return Ok(dto);
        }
    }
}