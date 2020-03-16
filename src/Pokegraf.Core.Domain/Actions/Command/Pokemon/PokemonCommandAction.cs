using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace Pokegraf.Core.Domain.Actions.Command.Pokemon
{
    public class PokemonCommandAction : CommandAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return Command.ToLower() == "/pokemon" || Command.ToLower() == "/pkm" || Command.ToLower() == "/pokémon";
        }
    }
}