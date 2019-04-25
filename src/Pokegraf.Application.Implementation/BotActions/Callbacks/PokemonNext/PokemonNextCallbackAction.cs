using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action;
using Pokegraf.Application.Contract.Model.Action.Callback;

namespace Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonNext
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