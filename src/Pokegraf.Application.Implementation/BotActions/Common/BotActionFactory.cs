using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Newtonsoft.Json;
using Pokegraf.Application.Contract.BotActions.Common;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Pokegraf.Common.Result;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Application.Implementation.BotActions.Common
{
    public class BotActionFactory : IBotActionFactory
    {
        public Result<IBotAction> GetBotAction(Message message)
        {
            var command = GetCommand(message);

            if (command == null) return Result<IBotAction>.NotFound(new List<string> {"No corresponding action found."});
            
            var botAction = message.ToBotAction();

            switch (command.ToLower())
            {
                case "/pokemon":
                case "/pkm":
                    return Result<IBotAction>.Success(botAction.ToPokemonCommandAction());
                case "/fusion":
                    return Result<IBotAction>.Success(botAction.ToFusionCommandAction());
                case "/about":
                    return Result<IBotAction>.Success(botAction.ToAboutCommandAction());
                case "/start":
                    return Result<IBotAction>.Success(botAction.ToStartCommandAction());
                default:
                    return Result<IBotAction>.NotFound(new List<string> {"No corresponding action found."});
            }
        }

        public Result<ICallbackAction> GetCallbackAction(CallbackQuery callbackQuery)
        {
            var callbackAction = callbackQuery.ToCallbackAction();

            if (!callbackAction.Data.ContainsKey("action"))
            {
                return Result<ICallbackAction>.NotFound(new List<string> {"No corresponding action found."});
            }
            
            switch (callbackAction.Data["action"])
            {
                case "pokemon_before":
                    return Result<ICallbackAction>.Success(callbackAction.ToPokemonBeforeCallbackAction());
                case "pokemon_next":
                    return Result<ICallbackAction>.Success(callbackAction.ToPokemonNextCallbackAction());
                case "pokemon_stats":
                    return Result<ICallbackAction>.Success(callbackAction.ToPokemonStatsCallbackAction());
                case "pokemon_description":
                    return Result<ICallbackAction>.Success(callbackAction.ToPokemonDescriptionCallbackAction());
                default:
                    return Result<ICallbackAction>.NotFound(new List<string> {"No corresponding action found."});
            }
        }

        
        private string GetCommand(Message message)
        {
            if (message.Entities?.First()?.Type != MessageEntityType.BotCommand) return null;
            
            var command = message.EntityValues.First();

            if (!command.Contains('@')) return command;
                
            if (!command.ToLower().Contains("@pokegraf_bot")) return null;

            command = command.Substring(0, command.IndexOf('@'));

            return command;
        }
    }
}