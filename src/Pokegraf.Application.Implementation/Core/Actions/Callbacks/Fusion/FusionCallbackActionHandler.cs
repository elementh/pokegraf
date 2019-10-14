using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pokegraf.Application.Implementation.Core.Responses.PhotoWithKeyboard.Edit;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.Core.Actions.Callbacks.Fusion
{
    public class FusionCallbackActionHandler : Pokegraf.Common.Request.CommonHandler<FusionCallbackAction, Result>
    {
        private readonly IPokemonService _pokemonService;

        public FusionCallbackActionHandler(ILogger<Pokegraf.Common.Request.CommonHandler<FusionCallbackAction, Result>> logger,
            IMediator mediatR, IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override async Task<Result> Handle(FusionCallbackAction request, CancellationToken cancellationToken)
        {
            var fusionResult = _pokemonService.GetFusion();

            if (!fusionResult.Succeeded) return fusionResult;

            var fusionCallback = new Dictionary<string, string>
            {
                {"action", "fusion"}
            };

            return await MediatR.Send(new EditPhotoWithCaptionWithKeyboardResponse(fusionResult.Value.Image.ToString(),
                fusionResult.Value.Name, new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("More fusion!",
                    JsonConvert.SerializeObject(fusionCallback))), request.MessageId));
        }
    }
}