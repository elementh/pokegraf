using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Contract.Model.Action.Callback;

namespace Pokegraf.Application.Implementation.Actions.Callbacks.PokemonNext
{
    public class PokemonNextCallbackAction : CallbackAction
    {
        public PokemonNextCallbackAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "pokemon_next";
        }
    }
}