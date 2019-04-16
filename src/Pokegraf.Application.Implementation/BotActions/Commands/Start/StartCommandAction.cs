using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Implementation.BotActions.Common;

namespace Pokegraf.Application.Implementation.BotActions.Commands.Start
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