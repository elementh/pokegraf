using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Responses.Text;
using Pokegraf.Application.Contract.Core.Responses.Text.WithKeyboard.Edit;
using Pokegraf.Common.ErrorHandling;
using static OperationResult.Helpers;

namespace Pokegraf.Application.Implementation.Core.Actions.Callbacks.Start
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
                return await Mediator.Send(new TextWithKeyboardEditResponse(request.MessageId, null, "No problem Trainer! Use /setname `CUSTOM_NAME_HERE` to set a custom name!\n\nAnd when you are done try doing /pokemon"), cancellationToken);
            }

            if (!request.Data.ContainsKey("requested_name")) return Ok();

            await Mediator.Send(request.MapToUpdateUserTrainerNameCommand());

            return await Mediator.Send(
                new TextWithKeyboardEditResponse(request.MessageId, null, $"Alright {request.Data.GetValueOrDefault("requested_name")}, why don't you try doing /pokemon ?"),
                cancellationToken);
        }
    }
}