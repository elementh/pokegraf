using Pokegraf.Application.Contract.BotActions.Common;
using Pokegraf.Application.Contract.Common.Context;

namespace Pokegraf.Application.Implementation.BotActions.Common
{
    public abstract class InlineAction : BotAction, IInlineAction
    {
        public InlineAction(IBotContext botContext) : base(botContext)
        {
        }
    }
}