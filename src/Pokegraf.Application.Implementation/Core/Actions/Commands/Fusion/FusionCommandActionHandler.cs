using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pokegraf.Application.Implementation.Core.Responses.PhotoWithKeyboard.Send;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.Core.Actions.Commands.Fusion
{
    public class FusionCommandActionHandler : Pokegraf.Common.Request.CommonHandler<FusionCommandAction, Result>
    {
        private readonly IPokemonService _pokemonService;

        public FusionCommandActionHandler(ILogger<Pokegraf.Common.Request.CommonHandler<FusionCommandAction, Result>> logger,
            IMediator mediatR, IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override async Task<Result> Handle(FusionCommandAction request, CancellationToken cancellationToken)
        {
            var fusionResult = _pokemonService.GetFusion();

            if (!fusionResult.Succeeded) return fusionResult;

            var fusionCallback = new Dictionary<string, string>
            {
                {"action", "fusion"}
            };

            return await MediatR.Send(new PhotoWithCaptionWithKeyboardResponse(fusionResult.Value.Image.ToString(),
                fusionResult.Value.Name, new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("More fusion!",
                    JsonConvert.SerializeObject(fusionCallback)))));
        }
    }
}