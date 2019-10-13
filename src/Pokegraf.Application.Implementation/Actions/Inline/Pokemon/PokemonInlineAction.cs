using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Application.Contract.Model.Action.Inline;

namespace Pokegraf.Application.Implementation.Actions.Inline.Pokemon
{
    public class PokemonInlineAction : InlineAction
    {
        public PokemonInlineAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return !string.IsNullOrWhiteSpace(condition);
        }
    }
}