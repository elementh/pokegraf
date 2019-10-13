using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Contract.Model.Action.Command
{
    public abstract class CommandAction : BotAction, ICommandAction
    {
        public CommandAction(IBotContext botContext) : base(botContext)
        {
        }
    }
}