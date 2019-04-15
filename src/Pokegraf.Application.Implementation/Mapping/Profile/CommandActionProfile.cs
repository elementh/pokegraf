using Pokegraf.Application.Implementation.BotActions.Commands.About;
using Pokegraf.Application.Implementation.BotActions.Commands.Fusion;
using Pokegraf.Application.Implementation.BotActions.Commands.Pokemon;
using Pokegraf.Application.Implementation.BotActions.Commands.Start;
using Pokegraf.Application.Implementation.BotActions.Common;

namespace Pokegraf.Application.Implementation.Mapping.Profile
{
    public class CommandActionProfile : AutoMapper.Profile
    {
        public CommandActionProfile()
        {
            CreateMap<CommandAction, AboutCommandAction>();
            CreateMap<CommandAction, StartCommandAction>();
            CreateMap<CommandAction, PokemonCommandAction>();
            CreateMap<CommandAction, FusionCommandAction>();
        }
    }
}