using Pokegraf.Application.Contract.Model;
using Pokegraf.Infrastructure.Contract.Dto;

namespace Pokegraf.Application.Implementation.Mapping.Profile
{
    public class IntentDtoProfile : AutoMapper.Profile
    {
        public IntentDtoProfile()
        {
            CreateMap<IntentDto, Intent>();
        }
    }
}