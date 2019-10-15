using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Responses.Text;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.User.Query.FindUser;

namespace Pokegraf.Application.Implementation.Core.Actions.Commands.SetTrainerName
{
    public class SetTrainerNameCommandActionHandler : IRequestHandler<SetTrainerNameCommandAction, Status<Error>>
    {
        protected readonly ILogger<SetTrainerNameCommandActionHandler> Logger;
        protected readonly IMediator Mediator;

        public SetTrainerNameCommandActionHandler(ILogger<SetTrainerNameCommandActionHandler> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        public async Task<Status<Error>> Handle(SetTrainerNameCommandAction request, CancellationToken cancellationToken)
        {
            var commandArgs = request.Text?.Split(" ");
            
            if (commandArgs != null && commandArgs.Length > 1)
            {
                var newName = commandArgs[1];
                await Mediator.Send(request.MapToUpdateUserTrainerNameCommand(newName), cancellationToken);

                var user = await Mediator.Send(new FindUserQuery{ UserId = request.From.Id}, cancellationToken);

                if (user.IsSuccess && user.Value?.TrainerName == newName)
                {
                    return await Mediator.Send(new TextResponse($"Alright *{user.Value.TrainerName}*, your new name has been set!"), cancellationToken);
                }
            }
            
            return await Mediator.Send(new TextResponse("Sorry but that name is too weird! Try again *Trainer*!"), cancellationToken);
        }
    }
}