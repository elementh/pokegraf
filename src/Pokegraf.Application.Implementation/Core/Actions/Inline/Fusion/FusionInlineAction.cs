using Pokegraf.Application.Contract.Core.Action.Inline;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Actions.Inline.Fusion
{
    public class FusionInlineAction : InlineAction
    {
        public FusionInlineAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return string.IsNullOrWhiteSpace(condition);
        }
    }
}