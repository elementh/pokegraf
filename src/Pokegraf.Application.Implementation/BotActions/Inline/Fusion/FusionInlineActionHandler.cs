using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Common.Responses.Inline;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.InlineQueryResults;

namespace Pokegraf.Application.Implementation.BotActions.Inline.Fusion
{
    public class FusionInlineActionHandler : Pokegraf.Common.Request.RequestHandler<FusionInlineAction, Result>
    {
        private readonly IPokemonService _pokemonService;

        public FusionInlineActionHandler(ILogger<Pokegraf.Common.Request.RequestHandler<FusionInlineAction, Result>> logger, IMediator mediatR, IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override Task<Result> Handle(FusionInlineAction request, CancellationToken cancellationToken)
        {
            InlineQueryResultBase[] results =
            {
                new InlineQueryResultPhoto($"pokemon:{result.Value.Id}", result.Value.Image.ToString(), result.Value.Sprite.ToString())
                {
                    Caption = $"{result.Value.Description}"
                }
            };

            return await MediatR.Send(new InlineResponse(results), cancellationToken);
        }
    }
}