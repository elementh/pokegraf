using Pokegraf.Infrastructure.Contract.Dto;

namespace Pokegraf.Infrastructure.Contract.Service
{
    public interface IPokemonService
    {
        PokemonDto GetPokemon(int number);
        PokemonDto GetPokemon(string name);
    }
}