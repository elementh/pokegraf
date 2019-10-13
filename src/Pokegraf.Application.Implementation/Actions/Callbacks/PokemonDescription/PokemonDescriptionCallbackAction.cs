using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Contract.Model.Action.Callback;

namespace Pokegraf.Application.Implementation.Actions.Callbacks.PokemonDescription
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