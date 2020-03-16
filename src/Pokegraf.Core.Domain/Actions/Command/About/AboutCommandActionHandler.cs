using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Pokegraf.Core.Domain.Resources;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Core.Domain.Actions.Command.About
{
    public class AboutCommandActionHandler : ActionHandler<AboutCommandAction>
    {
        public AboutCommandActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(AboutCommandAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), 
                PokegrafCoreDomainResources.AboutText, 
                ParseMode.Markdown,
                cancellationToken: cancellationToken);
        
            return default;
        }
    }
}