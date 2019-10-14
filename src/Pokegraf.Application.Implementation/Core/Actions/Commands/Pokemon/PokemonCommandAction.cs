using Pokegraf.Application.Contract.Core.Action.Command;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Actions.Commands.Pokemon
{
    public class PokemonCommandAction : CommandAction
    {
        public PokemonCommandAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "/pokemon" || condition == "/pkm";
        }
    }
}