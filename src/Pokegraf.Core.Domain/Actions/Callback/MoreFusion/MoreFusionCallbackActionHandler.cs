using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Pokegraf.Core.Domain.Extensions;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types;

namespace Pokegraf.Core.Domain.Actions.Callback.MoreFusion
{
    public class MoreFusionCallbackActionHandler : ActionHandler<MoreFusionCallbackAction>
    {
        protected readonly IPokemonService PokemonService;

        public MoreFusionCallbackActionHandler(INavigatorContext ctx, IPokemonService pokemonService) : base(ctx)
        {
            PokemonService = pokemonService;
        }

        public override async Task<Unit> Handle(MoreFusionCallbackAction request, CancellationToken cancellationToken)
        {
            var fusionResult = PokemonService.GetFusion();

            var photo = new InputMediaPhoto(fusionResult.Image.ToString());

            if (string.IsNullOrWhiteSpace(Ctx.GetCallbackQuery().InlineMessageId))
            {
                await Ctx.Client.EditMessageMediaAsync(Ctx.GetTelegramChat(), Ctx.GetCallbackQuery().Message.MessageId, photo,
                    fusionResult.ToInlineQueryResultPhoto().ReplyMarkup, cancellationToken);

                await Ctx.Client.EditMessageCaptionAsync(Ctx.GetTelegramChat(), Ctx.GetCallbackQuery().Message.MessageId, fusionResult.Name,
                    fusionResult.ToInlineQueryResultPhoto().ReplyMarkup, cancellationToken);
            }
            else
            {
                await Ctx.Client.EditMessageMediaAsync(Ctx.GetCallbackQuery().InlineMessageId, photo,
                    fusionResult.ToInlineQueryResultPhoto().ReplyMarkup, cancellationToken);

                await Ctx.Client.EditMessageCaptionAsync(Ctx.GetCallbackQuery().InlineMessageId, fusionResult.Name,
                    fusionResult.ToInlineQueryResultPhoto().ReplyMarkup, cancellationToken);
            }

            return default;
        }
    }
}