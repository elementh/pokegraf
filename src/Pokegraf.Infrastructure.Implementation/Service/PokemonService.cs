using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PokeAPI;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Pokegraf.Infrastructure.Contract.Service;
using System;
using System.Threading.Tasks;
using Pokegraf.Common.Helper;
using Pokegraf.Infrastructure.Implementation.Helper;

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

        public async Task<PokemonDto?> GetPokemon(int pokeNumber)
        {
            var rawCachedPokemon = await Cache.GetStringAsync(CacheKeys.Pokemon(pokeNumber));

            if (rawCachedPokemon != null)
            {
                Logger.LogTrace("Found pokemon {@PokemonId} in cache.", pokeNumber);
                
                var cachedPokemon = JsonConvert.DeserializeObject<PokemonDto>(rawCachedPokemon);
                
                return cachedPokemon;
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
                    return default;
                }
                
                Logger.LogError(e, "Unhandled error getting pokemon number {@PokeNumber}", pokeNumber);

                return default;
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

            await Cache.SetStringAsync(CacheKeys.Pokemon(dto.Id), JsonConvert.SerializeObject(dto));
            
            return dto;
        }

        public async Task<PokemonDto?> GetPokemon(string pokeName)
        {
            Pokemon pokemon;
            
            try
            {
                pokemon = await DataFetcher.GetNamedApiObject<Pokemon>(pokeName);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error getting pokemon named {@PokeName}", pokeName);

                return default;
            }

            return await GetPokemon(pokemon.ID);
        }

        public PokemonFusionDto GetFusion()
        {
            return PokeHelper.GetFusion();
        }
    }
}