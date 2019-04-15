using Pokegraf.Application.Implementation.BotActions.Common;
using Pokegraf.Application.Implementation.Common.Actions;

namespace Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonBefore
{
    public class PokemonBeforeCallbackAction : CallbackAction
    {
        public override bool CanHandle(string condition)
        {
            return condition == "pokemon_before";
        }
    }
}