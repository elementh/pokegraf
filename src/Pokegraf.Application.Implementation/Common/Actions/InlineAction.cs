using Pokegraf.Application.Contract.Common.Actions;
using Pokegraf.Application.Contract.Common.Context;

namespace Pokegraf.Application.Implementation.Common.Actions
{
    public abstract class InlineAction : BotAction, IInlineAction
    {
        public InlineAction(IBotContext botContext) : base(botContext)
        {
        }
    }
}