using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace Pokegraf.Core.Domain.Actions.InlineQuery.Fusion
{
    public class FusionInlineQueryAction : InlineQueryAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return string.IsNullOrWhiteSpace(Query);
        }
    }
}