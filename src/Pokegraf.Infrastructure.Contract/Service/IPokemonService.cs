using System.Threading.Tasks;
using OperationResult;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;

namespace Pokegraf.Infrastructure.Contract.Service
{
    public interface IPokemonService
    {
        Task<Result<PokemonDto, ResultError>> GetPokemon(int pokeNumber);
        Task<Result<PokemonDto, ResultError>> GetPokemon(string pokeName);
        Result<PokemonFusionDto, ResultError> GetFusion();
        Task<Result<BerryDto, ResultError>> GetBerry(string berryName);
    }
}