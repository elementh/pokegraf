using Pokegraf.Application.Implementation.BotActions.Callbacks.Fusion;
using Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonBefore;
using Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonDescription;
using Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonNext;
using Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonStats;
using Pokegraf.Application.Implementation.BotActions.Common;
using Pokegraf.Application.Implementation.Common.Actions;

namespace Pokegraf.Application.Implementation.Mapping.Profile
{
    public class CallbackActionProfile : AutoMapper.Profile
    {
        public CallbackActionProfile()
        {
            CreateMap<CallbackAction, PokemonBeforeCallbackAction>();
            CreateMap<CallbackAction, PokemonNextCallbackAction>();
            CreateMap<CallbackAction, PokemonStatsCallbackAction>();
            CreateMap<CallbackAction, PokemonDescriptionCallbackAction>();
            CreateMap<CallbackAction, FusionCallbackAction>();
        }
    }
}