using Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonBefore;
using Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonNext;
using Pokegraf.Application.Implementation.BotActions.Common;

namespace Pokegraf.Application.Implementation.Mapping.Profile
{
    public class CallbackActionProfile : AutoMapper.Profile
    {
        public CallbackActionProfile()
        {
            CreateMap<CallbackAction, PokemonBeforeCallbackAction>();
            CreateMap<CallbackAction, PokemonNextCallbackAction>();
        }
    }
}