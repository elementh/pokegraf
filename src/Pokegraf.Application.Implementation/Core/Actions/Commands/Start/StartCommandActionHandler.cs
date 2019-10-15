using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OperationResult;
using Pokegraf.Application.Contract.Core.Responses.Text.WithKeyboard;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Common.Helper;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

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
            var startText = "Hello there Pokémon Trainer! Welcome to *pokegraf*!\n\nWhat's your name?";

            // As listed in https://en.wikipedia.org/wiki/List_of_Pok%C3%A9mon_characters#Trainers

            return await Mediator.Send(new TextWithKeyboardResponse(GetKeyboard(), startText, ParseMode.Markdown));
        }

        protected InlineKeyboardMarkup GetKeyboard()
        {
            var defaultNames = new[]
            {
                // Protagonists
                "Red", "Brendan", "May", "Lucas", "Dawn", "Hilbert", "Hilda", "Nate", "Rosa", "Calem", "Serena", "Elio",
                "Selene", "Chase", "Elaine", "Victor", "Gloria",
                // Rivals
                "Blue", "Silver", "Wally", "Barry", "Cheren", "Bianca", "Hugh", "Shauna ", "Trevor", "Tierno", "Hau",
                "Gladion", "Trace", "Hop", "Bede", "Marnie",
                // Others
                "Brock", "Misty", "Giovanni"
            };

            var nameOne = defaultNames[RandomProvider.GetThreadRandom().Next(0, defaultNames.Length)];
            
            var nameTwo = defaultNames[RandomProvider.GetThreadRandom().Next(0, defaultNames.Length)];
            while (nameTwo == nameOne)
            {
                nameTwo = defaultNames[RandomProvider.GetThreadRandom().Next(0, defaultNames.Length)];
            }
            
            var nameThree = defaultNames[RandomProvider.GetThreadRandom().Next(0, defaultNames.Length)];
            while (nameThree == nameOne || nameThree == nameTwo)
            {
                nameThree = defaultNames[RandomProvider.GetThreadRandom().Next(0, defaultNames.Length)];
            }

            var customNameCallbackData = new Dictionary<string, string>
            {
                {"action", "start_set_trainer_name"}, 
                {"custom_name", "true"}
            };

            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(nameOne, JsonConvert.SerializeObject(GetRow(nameOne))),
                    InlineKeyboardButton.WithCallbackData(nameTwo, JsonConvert.SerializeObject(GetRow(nameTwo))),
                    InlineKeyboardButton.WithCallbackData(nameThree, JsonConvert.SerializeObject(GetRow(nameThree)))
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData($"I want a custom name!", JsonConvert.SerializeObject(customNameCallbackData)),
                }
            });
            
            Dictionary<string, string> GetRow(string name)
            {
                return new Dictionary<string, string>
                {
                    {"action", "start_set_trainer_name"}, 
                    {"requested_name", name}
                };
            }
        }
    }
}