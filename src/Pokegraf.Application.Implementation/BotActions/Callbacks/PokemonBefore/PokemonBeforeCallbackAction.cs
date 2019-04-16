using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Implementation.BotActions.Common;

namespace Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonBefore
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