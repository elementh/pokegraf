using System.Threading.Tasks;
using OperationResult;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;

namespace Pokegraf.Infrastructure.Contract.Service
{
    public interface IPokemonService
    {
        Task<PokemonDto?> GetPokemon(int pokeNumber);
        Task<PokemonDto?> GetPokemon(string pokeName);
        PokemonFusionDto GetFusion();
    }
}