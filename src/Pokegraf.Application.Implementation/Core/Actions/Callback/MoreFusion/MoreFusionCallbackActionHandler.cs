using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OperationResult;
using Pokegraf.Application.Implementation.Core.Responses.Photo.WithKeyboard;
using Pokegraf.Application.Implementation.Core.Responses.Photo.WithKeyboard.Edit;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.Stats.Command.AddOneToFusionRequests;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.ReplyMarkups;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Core.Actions.Callback.MoreFusion
{
    public class FusionCallbackActionHandler : IRequestHandler<MoreFusionCallbackAction, Status<Error>>
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

        public async Task<Status<Error>> Handle(MoreFusionCallbackAction request, CancellationToken cancellationToken)
        {
            var fusionResult = PokemonService.GetFusion();

            if (fusionResult.IsError) return Error(fusionResult.Error);

            var fusionCallback = new Dictionary<string, string>
            {
                {"action", "fusion"}
            };

            return await Mediator.Send(new PhotoWithKeyboardEditResponse(fusionResult.Value.Image.ToString(),
                fusionResult.Value.Name, new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("More fusion!",
                    JsonConvert.SerializeObject(fusionCallback))), request.MessageId), cancellationToken);
        }
    }
}