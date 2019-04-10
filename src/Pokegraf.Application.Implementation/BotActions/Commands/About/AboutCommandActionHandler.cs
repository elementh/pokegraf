using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.BotActions.Responses.Text;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.BotActions.Commands.About
{
    public class AboutCommandActionHandler : Pokegraf.Common.Request.RequestHandler<AboutCommandAction, Result>
    {
        public AboutCommandActionHandler(ILogger<Pokegraf.Common.Request.RequestHandler<AboutCommandAction, Result>> logger, IMediator mediatR) : base(logger, mediatR)
        {
        }

        public override async Task<Result> Handle(AboutCommandAction request, CancellationToken cancellationToken)
        {
            var aboutText =
                @"[pokegraf](https://github.com/elementh/pokegraf) is a bot made with ❤️ by [Lucas Maximiliano Marino](http://lucasmarino.me/) with the help of [contributors](https://github.com/elementh/pokegraf/blob/master/CONTRIBUTORS.md)!.  
It uses the [PokeAPI](https://github.com/PokeAPI/pokeapi) and [Pokemon Fusion](http://pokemon.alexonsager.net/).  
Pokémon ©1995 [pokémon](http://www.pokemon.com/), [nintendo](http://www.nintendo.com/), [game freak](http://www.gamefreak.co.jp/), [creatures](http://www.creatures.co.jp/html/en/).";

            return await MediatR.Send(new TextResponse(request.Chat.Id, aboutText));
        }
    }
}