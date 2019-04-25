using AutoMapper;
using Pokegraf.Application.Contract.Model;
using Pokegraf.Infrastructure.Contract.Dto;

namespace Pokegraf.Application.Implementation.Mapping.Extension
{
    public static class IntentDtoExtension
    {
        internal static IMapper Mapper { get; }
        
        static IntentDtoExtension()
        {
            Mapper = new MapperConfiguration(cfg => { }).CreateMapper();
        }
        
        public static Intent ToIntent(this IntentDto intent)
        {
            return intent != null
                ? Mapper.Map<Intent>(intent)
                : null;
        }
    }
}