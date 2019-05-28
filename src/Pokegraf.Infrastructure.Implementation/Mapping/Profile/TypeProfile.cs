using System.Linq;
using AutoMapper;
using PokeAPI;
using Pokegraf.Common.Helper;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;

namespace Pokegraf.Infrastructure.Implementation.Mapping.Profile
{
    public class TypeProfile : AutoMapper.Profile
    {
        public TypeProfile()
        {
            CreateMap<PokemonType, TypeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src =>
                    src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                    src.Name))
                .ForMember(dest => dest.Generation, opt => opt.MapFrom(src => 
                    src.Generation.Name))
                .ForMember(dest => dest.MoveDamageClass, opt => opt.MapFrom(src =>
                    src.MoveDamageClass))
                .ForMember(dest => dest.DoubleDamageFrom, opt => opt.MapFrom(src => 
                    src.DamageRelations.DoubleDamageFrom.Select(t => t.Name)))
                .ForMember(dest => dest.DoubleDamageTo, opt => opt.MapFrom(src => 
                    src.DamageRelations.DoubleDamageTo.Select(t => t.Name)))
                .ForMember(dest => dest.HalfDamageFrom, opt => opt.MapFrom(src => 
                    src.DamageRelations.HalfDamageFrom.Select(t => t.Name)))
                .ForMember(dest => dest.HalfDamageTo, opt => opt.MapFrom(src => 
                    src.DamageRelations.HalfDamageTo.Select(t => t.Name)))
                .ForMember(dest => dest.NoDamageFrom, opt => opt.MapFrom(src => 
                    src.DamageRelations.NoDamageFrom.Select(t => t.Name)))
                .ForMember(dest => dest.NoDamageTo, opt => opt.MapFrom(src => 
                    src.DamageRelations.NoDamageTo.Select(t => t.Name)))
                .ForMember(dest => dest.Pokemon, opt => opt.MapFrom(src =>
                    src.Pokemon.Select(t => t.Pokemon.Name.FirstLetterToUpperCase())));
        }
    }
}