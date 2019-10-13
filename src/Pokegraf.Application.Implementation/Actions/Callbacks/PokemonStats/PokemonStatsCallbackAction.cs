using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Contract.Model.Action.Callback;

namespace Pokegraf.Application.Implementation.Actions.Callbacks.PokemonStats
{
    public class PokemonStatsCallbackAction : CallbackAction
    {
        public PokemonStatsCallbackAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "pokemon_stats";
        }
    }
}