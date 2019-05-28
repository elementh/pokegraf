using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.Common.Responses.Text;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Dto.Pokemon;
using Pokegraf.Infrastructure.Contract.Service;

namespace Pokegraf.Application.Implementation.BotActions.Conversation.Berry.Flavor
{
    public class BerryFlavorConversationActionHandler : Pokegraf.Common.Request.RequestHandler<BerryFlavorConversationAction, Result>
    {
        private IPokemonService _pokemonService;
        
        public BerryFlavorConversationActionHandler(ILogger<Pokegraf.Common.Request.RequestHandler<BerryFlavorConversationAction, Result>> logger, IMediator mediatR, IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override async Task<Result> Handle(BerryFlavorConversationAction request, CancellationToken cancellationToken)
        {
            Result<BerryDto> berryResult;

            if (request.Parameters.TryGetValue("requested_berry_name", out var requestedBerryName) && !string.IsNullOrWhiteSpace(requestedBerryName))
            {
                berryResult = await _pokemonService.GetBerry(requestedBerryName.ToLower());
            }
            else
            {
                return await MediatR.Send(new TextResponse("Sorry, I couldn't find that berry, why don't you try another one?"));
            }
            
            if (!berryResult.Succeeded)
            {
                if (berryResult.Errors.ContainsKey("not_found"))
                {
                    return await MediatR.Send(new TextResponse("Sorry, I couldn't find that berry, why don't you try another one?"));
                }

                return berryResult;
            }

            return await MediatR.Send(new TextResponse(GenerateResponse(berryResult.Value)));
        }

        private static string GenerateResponse(BerryDto berry)
        {
            var description = new StringBuilder($"The **{berry.Name}** berry tastes mainly *{berry.Flavors.FirstOrDefault()}*");

            var flavorsCount = berry.Flavors.Count;

            foreach (var flavor in berry.Flavors)
            {
                if (berry.Flavors.IndexOf(flavor) == 0)
                {
                    continue;
                }
                
                if (berry.Flavors.IndexOf(flavor) == flavorsCount - 1)
                {
                    description.Append($" and a bit *{flavor}*");
                    continue;
                }

                if (flavorsCount <= 2) continue;
                
                if (berry.Flavors.IndexOf(flavor) == 1)
                {
                    description.Append($", but it also is *{flavor}*");
                    continue;
                }

                description.Append($", *{flavor}*");
            }

            description.Append(".");
            
            return description.ToString();
        }
    }
}