using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PokeAPI;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Dto;
using Pokegraf.Infrastructure.Contract.Service;

namespace Pokegraf.Infrastructure.Implementation.Service
{
    public class PokemonService : IPokemonService
    {
        protected readonly Logger<PokemonSpecies> Logger;

        public PokemonService(Logger<PokemonSpecies> logger)
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
                
                Logger.LogError($"Unhandled error getting pokemon number {pokeNumber}", e);
                
                return Result<PokemonDto>.UnknownError(new List<string> {$"Unhandled error getting pokemon number {pokeNumber}"});
            }
            
            var species = await DataFetcher.GetApiObject<PokemonSpecies>(pokeNumber);

            var dto = new PokemonDto()
            {
                Id = pokeNumber,
                Name = pokemon.Name,
                Description = await GetDescription(pokeNumber),
                Image = GetImageUri(pokeNumber),
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
                
                Logger.LogError($"Unhandled error getting pokemon named {pokeName}", e);
                
                return Result<PokemonDto>.UnknownError(new List<string> {$"Unhandled error getting pokemon number {pokeName}"});
            }

            return await GetPokemon(pokemon.ID);
        }

        protected async Task<string> GetDescription(int pokeNumber)
        {
            var species = await DataFetcher.GetApiObject<PokemonSpecies>(pokeNumber);

            return species.FlavorTexts.First(text => text.Language.Name == "en").FlavorText;
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
            
            return new Tuple<int, string>(pokeNumber, pokemon.Name);
        }
        
        protected async Task<Tuple<int, string>> GetPokemonBefore(int pokeNumber)
        {
            switch (pokeNumber)
            {
                case 1:
                    pokeNumber = 721;
                    break;
                default:
                    ++pokeNumber;
                    break;
            }

            var pokemon = await DataFetcher.GetApiObject<Pokemon>(pokeNumber);
            
            return new Tuple<int, string>(pokeNumber, pokemon.Name);
        }
    }
}