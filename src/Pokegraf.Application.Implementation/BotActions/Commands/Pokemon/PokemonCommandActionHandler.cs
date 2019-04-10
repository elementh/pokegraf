using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.BotActions.Responses.Keyboard.InlineKeyboard;
using Pokegraf.Application.Implementation.BotActions.Responses.Photo;
using Pokegraf.Application.Implementation.BotActions.Responses.Text;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Service;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pokegraf.Application.Implementation.BotActions.Commands.Pokemon
{
    public class PokemonCommandActionHandler : Pokegraf.Common.Request.RequestHandler<PokemonCommandAction, Result>
    {
        private readonly IPokemonService _pokemonService;
        
        public PokemonCommandActionHandler(ILogger<Pokegraf.Common.Request.RequestHandler<PokemonCommandAction, Result>> logger, IMediator mediatR, IPokemonService pokemonService) : base(logger, mediatR)
        {
            _pokemonService = pokemonService;
        }

        public override async Task<Result> Handle(PokemonCommandAction request, CancellationToken cancellationToken)
        {
            var commandArgs = request.Text?.Split(" ");

            string requestedPokemon;
            
            if (commandArgs != null && commandArgs.Length > 1)
            {
                requestedPokemon = commandArgs[1];
            }
            else
            {
                requestedPokemon = new Random().Next(1, 721).ToString();
            }

            var result = int.TryParse(requestedPokemon, out var pokeNumber)
                ? await _pokemonService.GetPokemon(pokeNumber)
                : await _pokemonService.GetPokemon(requestedPokemon);

            if (result.Succeeded)
            {
                var photoSentResult = await MediatR.Send(new PhotoWithCaptionResponse(request.Chat.Id, result.Value.Image.ToString(), 
                    $"{result.Value.Name}"));

                if (photoSentResult.Succeeded)
                {
                    return await MediatR.Send(new InlineKeyboardResponse(request.Chat.Id, result.Value.Description, new InlineKeyboardMarkup(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"⬅{result.Value.Before.Item2}", $"/pokemon {result.Value.Before.Item1}"),
                        InlineKeyboardButton.WithCallbackData($"{result.Value.Next.Item2}➡", $"/pokemon {result.Value.Next.Item1}")
                    })));
                }

                return photoSentResult;
            }

            if (result.Errors.ContainsKey("not_found"))
            {
                return await MediatR.Send(new TextResponse(request.Chat.Id, result.Errors["not_found"].First() ?? "Ups, there was an error! Try again later!"));
            }

            return result;

        }
    }
}