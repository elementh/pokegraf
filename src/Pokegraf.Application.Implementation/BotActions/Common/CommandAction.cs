using Pokegraf.Application.Contract.BotActions.Common;
using Pokegraf.Application.Contract.Common.Context;

namespace Pokegraf.Application.Implementation.BotActions.Common
{
    public abstract class CommandAction : BotAction, ICommandAction
    {
        public CommandAction(IBotContext botContext) : base(botContext)
        {
        }
    }
}