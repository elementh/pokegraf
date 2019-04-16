using Pokegraf.Application.Contract.Common.Actions;
using Pokegraf.Application.Contract.Common.Context;

namespace Pokegraf.Application.Implementation.Common.Actions
{
    public abstract class CommandAction : BotAction, ICommandAction
    {
        public CommandAction(IBotContext botContext) : base(botContext)
        {
        }
    }
}