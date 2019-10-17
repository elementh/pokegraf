using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Implementation.Core.Responses.Text;
using Pokegraf.Application.Implementation.Core.Responses.Text.WithKeyboard.Edit;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.User.Query.FindUser;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Core.Actions.Callback.Start
{
    public class StartCallbackActionHandler : IRequestHandler<StartCallbackAction, Status<Error>>
    {
        protected readonly ILogger<StartCallbackActionHandler> Logger;
        protected readonly IMediator Mediator;

        public StartCallbackActionHandler(ILogger<StartCallbackActionHandler> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        public async Task<Status<Error>> Handle(StartCallbackAction request, CancellationToken cancellationToken)
        {
            if (request.Data.ContainsKey("custom_name"))
            {
                return await Mediator.Send(new TextWithKeyboardEditResponse(request.MessageId, null, "No problem *Trainer*! Use /setname YOUR-NAME to set a custom name!\n\nAnd when you are done try doing /pokemon"), cancellationToken);
            }

            if (!request.Data.ContainsKey("requested_name")) return Ok();

            await Mediator.Send(request.MapToUpdateUserTrainerNameCommand(), cancellationToken);

            var user = await Mediator.Send(new FindUserQuery{ UserId = request.From.Id}, cancellationToken);

            if (user.IsSuccess && user.Value?.TrainerName == request.Data.GetValueOrDefault("requested_name"))
            {
                return await Mediator.Send(
                    new TextWithKeyboardEditResponse(request.MessageId, null, $"Alright *{user.Value.TrainerName}*, why don't you try doing /pokemon ?"),
                    cancellationToken);
            }
            
            return await Mediator.Send(new TextResponse("Oops, something went wrong *Trainer*! Try again /start"), cancellationToken);
        }
    }
}