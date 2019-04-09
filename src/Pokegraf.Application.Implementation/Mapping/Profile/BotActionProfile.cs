using Pokegraf.Application.Implementation.BotActions.Commands.About;
using Pokegraf.Application.Implementation.BotActions.Commands.Fusion;
using Pokegraf.Application.Implementation.BotActions.Commands.Pokemon;
using Pokegraf.Application.Implementation.BotActions.Common;

namespace Pokegraf.Application.Implementation.Mapping.Profile
{
    public class BotActionProfile : AutoMapper.Profile
    {
        public BotActionProfile()
        {
            CreateMap<BotAction, AboutCommandAction>();
            CreateMap<BotAction, PokemonCommandAction>();
            CreateMap<BotAction, FusionCommandAction>();
        }
    }
}