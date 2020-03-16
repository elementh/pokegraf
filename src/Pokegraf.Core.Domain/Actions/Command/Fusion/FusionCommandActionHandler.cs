using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Newtonsoft.Json;
using Pokegraf.Common.Resources;
using Pokegraf.Core.Domain.Extensions;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Core.Domain.Actions.Command.Fusion
{
    public class FusionCommandActionHandler : ActionHandler<FusionCommandAction>
    {
        protected readonly IPokemonService PokemonService;

        public FusionCommandActionHandler(INavigatorContext ctx, IPokemonService pokemonService) : base(ctx)
        {
            PokemonService = pokemonService;
        }

        public override async Task<Unit> Handle(FusionCommandAction request, CancellationToken cancellationToken)
        {
            var fusionResult = PokemonService.GetFusion();

            await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), fusionResult.Image.ToString(), fusionResult.Name, ParseMode.Markdown,
                replyMarkup: PokemonFusionDtoExtension.FusionKeyboard, cancellationToken: cancellationToken);

            return default;
        }
    }
}