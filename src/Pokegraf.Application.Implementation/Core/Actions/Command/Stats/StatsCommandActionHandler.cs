using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Service;
using Pokegraf.Application.Implementation.Core.Responses.Text;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.Stats.Query.FindUserStats;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.Core.Actions.Command.Stats
{
    public class StatsCommandActionHandler : IRequestHandler<StatsCommandAction, Status<Error>>
    {
        protected readonly ILogger<StatsCommandActionHandler> Logger;
        protected readonly IMediator Mediator;
        protected readonly IGlobalStatsService GlobalStatsService;

        public StatsCommandActionHandler(ILogger<StatsCommandActionHandler> logger, IMediator mediator, IGlobalStatsService globalStatsService)
        {
            Logger = logger;
            Mediator = mediator;
            GlobalStatsService = globalStatsService;
        }

        public async Task<Status<Error>> Handle(StatsCommandAction request, CancellationToken cancellationToken)
        {
            var globalStats = await GlobalStatsService.Get(cancellationToken);
            if (globalStats.IsError)
            {
                return await Mediator.Send(new TextResponse($"Sorry {request.From.TrainerName}, seems like I can't get you this data right now! Try again later!"));
            }

            var messageBuilder = new StringBuilder();
            messageBuilder.AppendLine($"There are a total of {globalStats.Value.Users} trainers and {globalStats.Value.Chats} parties in @pokegraf_bot !");
            messageBuilder.AppendLine($"Trainers have requested pokemons a total of {globalStats.Value.PokemonRequests} times and fusions {globalStats.Value.FusionRequests} times!");
            
            var userStatsResult = await Mediator.Send(new FindUserStatsQuery {UserId = request.From.Id}, cancellationToken);
            if (userStatsResult.IsSuccess)
            {
                messageBuilder.AppendLine($"You have requested pokemons {userStatsResult.Value.Requests.Pokemon} times {userStatsResult.Value.Requests.Fusion} times. Great work {request.From.TrainerName}!");
            }
            
            return await Mediator.Send(new TextResponse(messageBuilder.ToString()));
        }
    }
}