using Pokegraf.Application.Contract.Action.Command;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Actions.Commands.Start
{
    public class StartCommandAction : CommandAction
    {
        public StartCommandAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "/start";
        }
    }
}