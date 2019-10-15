using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OperationResult;
using Pokegraf.Application.Implementation.Core.Responses.Photo.WithKeyboard;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.Stats.Command.AddOneToFusionRequests;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.ReplyMarkups;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Core.Actions.Callback.Fusion
{
    public class FusionCallbackActionHandler : IRequestHandler<FusionCallbackAction, Status<Error>>
    {
        protected readonly ILogger<FusionCallbackActionHandler> Logger;
        protected readonly IMediator Mediator;
        protected readonly IPokemonService PokemonService;

        public FusionCallbackActionHandler(ILogger<FusionCallbackActionHandler> logger, IMediator mediator, IPokemonService pokemonService)
        {
            Logger = logger;
            Mediator = mediator;
            PokemonService = pokemonService;
        }

        public async Task<Status<Error>> Handle(FusionCallbackAction request, CancellationToken cancellationToken)
        {
            var fusionResult = PokemonService.GetFusion();

            if (fusionResult.IsError) return Error(fusionResult.Error);

            var fusionCallback = new Dictionary<string, string>
            {
                {"action", "fusion"}
            };

            await Mediator.Send(new AddOneToFusionRequestsCommand {UserId = request.From.Id}, cancellationToken);

            return await Mediator.Send(new PhotoWithKeyboardResponse(fusionResult.Value.Image.ToString(),
                fusionResult.Value.Name, new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("More fusion!",
                    JsonConvert.SerializeObject(fusionCallback)))), cancellationToken);
        }
    }
}