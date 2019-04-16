using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Implementation.BotActions.Common;

namespace Pokegraf.Application.Implementation.BotActions.Inline.Pokemon
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