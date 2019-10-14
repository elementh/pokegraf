using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Core.Responses.Text;

namespace Pokegraf.Application.Implementation.Core.Actions.Commands.Start
{
    public class StartCommandActionHandler : Pokegraf.Common.Request.CommonHandler<StartCommandAction, Result>
    {
        public StartCommandActionHandler(ILogger<Pokegraf.Common.Request.CommonHandler<StartCommandAction, Result>> logger, IMediator mediatR) : base(logger, mediatR)
        {
            
        }

        public override async Task<Result> Handle(StartCommandAction request, CancellationToken cancellationToken)
        {
            var startText = "Hello there Pokémon Trainer! Welcome to *pokegraf*!\n\nWhy don't you try doing /pokemon ?";

            return await MediatR.Send(new TextResponse(startText));
        }
    }
}