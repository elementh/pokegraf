using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Implementation.BotActions.Common;

namespace Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonDescription
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