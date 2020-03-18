using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Pokegraf.Common.Resources;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Core.Domain.Actions.Command.Start
{
    public class StartCommandActionHandler : ActionHandler<StartCommandAction>
    {
        public StartCommandActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(StartCommandAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), 
                PokegrafResources.StartText, 
                ParseMode.Markdown,
                cancellationToken: cancellationToken);
        
            return default;
        }
    }
}