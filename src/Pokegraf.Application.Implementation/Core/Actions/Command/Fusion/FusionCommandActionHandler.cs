using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OperationResult;
using Pokegraf.Application.Contract.Core.Responses.Photo.WithKeyboard;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.ReplyMarkups;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Core.Actions.Command.Fusion
{
    public class FusionCommandActionHandler : IRequestHandler<FusionCommandAction, Status<Error>>
    {
        protected readonly ILogger<FusionCommandActionHandler> Logger;
        protected readonly IMediator Mediator;
        protected readonly IPokemonService PokemonService;

        public FusionCommandActionHandler(ILogger<FusionCommandActionHandler> logger, IMediator mediator, IPokemonService pokemonService)
        {
            Logger = logger;
            Mediator = mediator;
            PokemonService = pokemonService;
        }

        public async Task<Status<Error>> Handle(FusionCommandAction request, CancellationToken cancellationToken)
        {
            var fusionResult = PokemonService.GetFusion();

            if (fusionResult.IsError) return Error(fusionResult.Error);

            var fusionCallback = new Dictionary<string, string>
            {
                {"action", "fusion"}
            };

            return await Mediator.Send(new PhotoWithKeyboardResponse(fusionResult.Value.Image.ToString(),
                fusionResult.Value.Name, new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("More fusion!",
                    JsonConvert.SerializeObject(fusionCallback)))));
        }
    }
}