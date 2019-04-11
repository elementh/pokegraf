using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pokegraf.Application.Implementation.BotActions.Responses.Keyboard.InlineKeyboard;
using Pokegraf.Application.Implementation.BotActions.Responses.Photo;
using Pokegraf.Application.Implementation.BotActions.Responses.Text;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Dto;
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

            if (!result.Succeeded)
            {
                if (result.Errors.ContainsKey("not_found"))
                {
                    return await MediatR.Send(new TextResponse(request.Chat.Id,
                        result.Errors["not_found"].First() ?? "Ups, there was an error! Try again later!"));
                }

                return result;
            }

            var photoSentResult = await MediatR.Send(new PhotoWithCaptionResponse(request.Chat.Id, result.Value.Image.ToString(), 
                $"{result.Value.Name}"));

            if (!photoSentResult.Succeeded) return photoSentResult;

            var keyboard = GetKeyboard(result.Value);
            
            return await MediatR.Send(new InlineKeyboardResponse(request.Chat.Id, result.Value.Description, keyboard));
        }

        private InlineKeyboardMarkup GetKeyboard(PokemonDto pokemon)
        {
            var pokemonBeforeCallback = new OrderedDictionary
            {
                {"callback_action", "pokemonBefore"}, 
                {"requested_pokemon", pokemon.Before.Item1}
            };
            
            var pokemonNextCallback = new OrderedDictionary
            {
                {"callback_action", "pokemonNext"},
                {"requested_pokemon", pokemon.Next.Item1}
            };

            return new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData($"⬅{pokemon.Before.Item2}", JsonConvert.SerializeObject(pokemonBeforeCallback)),
                InlineKeyboardButton.WithCallbackData($"{pokemon.Next.Item2}➡", JsonConvert.SerializeObject(pokemonNextCallback))
            });
        }
    }
}