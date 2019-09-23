using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action.Command;

namespace Pokegraf.Application.Implementation.Actions.Commands.About
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