using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Contract.Core.Responses.Text;
using Pokegraf.Application.Implementation.Core.Responses.Text;
using Pokegraf.Common.ErrorHandling;

namespace Pokegraf.Application.Implementation.Core.Actions.Commands.Start
{
    public class StartCommandActionHandler : IRequestHandler<StartCommandAction, Status<Error>>
    {
        protected readonly ILogger<StartCommandActionHandler> Logger;
        protected readonly IMediator Mediator;

        public StartCommandActionHandler(ILogger<StartCommandActionHandler> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        public async Task<Status<Error>> Handle(StartCommandAction request, CancellationToken cancellationToken)
        {
            var startText = "Hello there Pokémon Trainer! Welcome to *pokegraf*!\n\nWhy don't you try doing /pokemon ?";

            return await Mediator.Send(new TextResponse(startText));
        }
    }
}