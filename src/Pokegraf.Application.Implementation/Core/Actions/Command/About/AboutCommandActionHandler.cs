using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Pokegraf.Application.Implementation.Core.Responses.Text;
using Pokegraf.Common.ErrorHandling;

namespace Pokegraf.Application.Implementation.Core.Actions.Command.About
{
    public class AboutCommandActionHandler : IRequestHandler<AboutCommandAction, Status<Error>>
    {
        protected readonly ILogger<AboutCommandActionHandler> Logger;
        protected readonly IMediator Mediator;

        public AboutCommandActionHandler(ILogger<AboutCommandActionHandler> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        public async Task<Status<Error>> Handle(AboutCommandAction request, CancellationToken cancellationToken)
        {
            var aboutText =
                @"[pokegraf](https://github.com/elementh/pokegraf) is a bot made with ❤️ by [Lucas Maximiliano Marino](http://lucasmarino.me/) with the help of [contributors](https://github.com/elementh/pokegraf/blob/master/CONTRIBUTORS.md)!.  
It uses the [PokeAPI](https://github.com/PokeAPI/pokeapi) and [Pokemon Fusion](http://pokemon.alexonsager.net/).  
Pokémon ©1995 [pokémon](http://www.pokemon.com/), [nintendo](http://www.nintendo.com/), [game freak](http://www.gamefreak.co.jp/), [creatures](http://www.creatures.co.jp/html/en/).";

            return await Mediator.Send(new TextResponse(aboutText));
        }
    }
}