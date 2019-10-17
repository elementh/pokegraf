using System.Threading.Tasks;
using OperationResult;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;

namespace Pokegraf.Infrastructure.Contract.Service
{
    public interface IPokemonService
    {
        Task<Result<PokemonDto, Error>> GetPokemon(int pokeNumber);
        Task<Result<PokemonDto, Error>> GetPokemon(string pokeName);
        Result<PokemonFusionDto, Error> GetFusion();
        Task<Result<BerryDto, Error>> GetBerry(string berryName);
    }
}