using System.Linq;
using AutoMapper;
using PokeAPI;
using Pokegraf.Common.Helper;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Pokegraf.Infrastructure.Implementation.Mapping.Profile;

namespace Pokegraf.Infrastructure.Implementation.Mapping.Extension
{
    public static class BerryExtension
    {
        internal static IMapper Mapper { get; }

        static BerryExtension()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BerryProfile>();
            }).CreateMapper();
        }

        public static BerryDto ToBerryDto(this Berry berry, PokemonType type)
        {
            if (berry == null || type == null)
                return null;
            
            var berryDto = Mapper.Map<BerryDto>(berry);
            
            berryDto.NaturalGiftType = type.ToTypeDto();

            return berryDto;
        }

    }
}