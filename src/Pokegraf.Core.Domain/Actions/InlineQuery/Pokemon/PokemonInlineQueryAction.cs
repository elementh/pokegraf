using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace Pokegraf.Core.Domain.Actions.InlineQuery.Pokemon
{
    public class PokemonInlineQueryAction : InlineQueryAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return !string.IsNullOrWhiteSpace(Query);
        }
    }
}