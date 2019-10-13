using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Contract.Model.Action.Command;

namespace Pokegraf.Application.Implementation.Actions.Commands.Pokemon
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