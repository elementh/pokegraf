using Pokegraf.Application.Contract.Action.Callback;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Actions.Callbacks.PokemonBefore
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