using Pokegraf.Application.Contract.Core.Action.Callback;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Actions.Callbacks.PokemonBefore
{
    public class PokemonBeforeCallbackAction : CallbackAction
    {
        public PokemonBeforeCallbackAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "pokemon_before";
        }
    }
}