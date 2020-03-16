using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace Pokegraf.Core.Domain.Actions.Command.About
{
    public class AboutCommandAction : CommandAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return Command == "/about";
        }
    }
}