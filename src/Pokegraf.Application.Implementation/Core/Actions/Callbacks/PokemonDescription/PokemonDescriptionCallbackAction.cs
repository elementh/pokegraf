using Pokegraf.Application.Contract.Core.Action.Callback;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Actions.Callbacks.PokemonDescription
{
    public class PokemonDescriptionCallbackAction : CallbackAction
    {
        public PokemonDescriptionCallbackAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "pokemon_description";
        }
    }
}