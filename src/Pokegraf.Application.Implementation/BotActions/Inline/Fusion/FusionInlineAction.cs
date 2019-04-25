using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action;
using Pokegraf.Application.Contract.Model.Action.Inline;

namespace Pokegraf.Application.Implementation.BotActions.Inline.Fusion
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