using System.Linq;
using PokeAPI;
using Pokegraf.Common.Helper;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;

namespace Pokegraf.Infrastructure.Implementation.Mapping.Profile
{
    public class BerryProfile : AutoMapper.Profile
    {
        public BerryProfile()
        {
            CreateMap<Berry, BerryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src =>
                    src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                    src.Name.FirstLetterToUpperCase()))
                .ForMember(dest => dest.GrowthTime, opt => opt.MapFrom(src =>
                    src.GrowthTime))
                .ForMember(dest => dest.NaturalGiftType, opt => opt.Ignore())
                .ForMember(dest => dest.Flavors, opt => opt.MapFrom(src =>
                    src.Flavors.Where(berryFlavor => berryFlavor.Potency > 0)
                        .OrderByDescending(berryFlavor => berryFlavor.Potency)
                        .Select(berryFlavor => berryFlavor.Flavor.Name).ToList()))
                .ForMember(dest => dest.Firmness, opt => opt.MapFrom(src =>
                    src.Firmness.Name))
                .ForMember(dest => dest.MaxHarvest, opt => opt.MapFrom(src =>
                    src.MaxHarvest))
                .ForMember(dest => dest.Smoothness, opt => opt.MapFrom(src =>
                    src.Smoothness));
        }
    }
}