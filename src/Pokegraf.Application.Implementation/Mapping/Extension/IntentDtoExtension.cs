using AutoMapper;
using Pokegraf.Application.Contract.Model;
using Pokegraf.Application.Implementation.Mapping.Profile;
using Pokegraf.Infrastructure.Contract.Dto;

namespace Pokegraf.Application.Implementation.Mapping.Extension
{
    public static class IntentDtoExtension
    {
        internal static IMapper Mapper { get; }
        
        static IntentDtoExtension()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<IntentDtoProfile>();
            }).CreateMapper();
        }
        
        public static Intent ToIntent(this IntentDto intent)
        {
            return intent != null
                ? Mapper.Map<Intent>(intent)
                : null;
        }
    }
}