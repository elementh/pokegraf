using AutoMapper;
using PokeAPI;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Pokegraf.Infrastructure.Implementation.Mapping.Profile;

namespace Pokegraf.Infrastructure.Implementation.Mapping.Extension
{
    public static class TypeExtension
    {
        internal static IMapper Mapper { get; }
        
        static TypeExtension()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TypeProfile>();
            }).CreateMapper();
        }

        public static TypeDto ToTypeDto(this PokemonType type)
        {
            return type != null
                ? Mapper.Map<TypeDto>(type)
                : null;
        }
    }
}