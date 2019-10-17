using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OperationResult;
using PokeAPI;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Pokegraf.Infrastructure.Contract.Service;
using Pokegraf.Infrastructure.Implementation.Helper;
using Pokegraf.Infrastructure.Implementation.Mapping.Extension;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Infrastructure.Implementation.Service
{
    public class PokemonService : IPokemonService
    {
        protected readonly ILogger<PokemonSpecies> Logger;

        public PokemonService(ILogger<PokemonSpecies> logger)
        {
            Logger = logger;
        }

        public async Task<Result<PokemonDto, Error>> GetPokemon(int pokeNumber)
        {
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
            
            var dto = new PokemonDto()
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

            return Ok(dto);
        }
    }
}