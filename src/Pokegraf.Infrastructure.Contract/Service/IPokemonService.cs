using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Dto;

namespace Pokegraf.Infrastructure.Contract.Service
{
    public interface IPokemonService
    {
        Task<Result<PokemonDto>> GetPokemon(int pokeNumber);
        Task<Result<PokemonDto>> GetPokemon(string pokeName);
        Result<PokemonFusionDto> GetFusion();
    }
}