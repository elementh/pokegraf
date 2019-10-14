using Pokegraf.Application.Contract.Core.Action.Command;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Actions.Commands.About
{
    public class AboutCommandAction : CommandAction
    {
        public AboutCommandAction(IBotContext botContext) : base(botContext)
        {
        }

        public override bool CanHandle(string condition)
        {
            return condition == "/about";
        }
    }
}