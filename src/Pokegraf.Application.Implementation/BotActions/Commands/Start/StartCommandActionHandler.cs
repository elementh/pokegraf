using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Common.Responses.Text;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.BotActions.Commands.Start
{
    public class StartCommandActionHandler : Pokegraf.Common.Request.RequestHandler<StartCommandAction, Result>
    {
        public StartCommandActionHandler(ILogger<Pokegraf.Common.Request.RequestHandler<StartCommandAction, Result>> logger, IMediator mediatR) : base(logger, mediatR)
        {
            
        }

        public override async Task<Result> Handle(StartCommandAction request, CancellationToken cancellationToken)
        {
            var startText = "Hello there Pokémon Trainer! Welcome to *pokegraf*!\n\nWhy don't you try doing /pokemon ?";

            return await MediatR.Send(new TextResponse(request.Chat.Id, startText));
        }
    }
}