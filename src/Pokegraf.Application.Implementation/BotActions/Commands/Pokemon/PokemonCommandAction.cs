using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Implementation.BotActions.Common;

namespace Pokegraf.Application.Implementation.BotActions.Commands.Pokemon
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