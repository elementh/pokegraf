using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OperationResult;
using Pokegraf.Application.Contract.Action.Callback;
using Pokegraf.Application.Contract.Action.Command;
using Pokegraf.Application.Contract.Action.Inline;
using Pokegraf.Application.Contract.Action.Update;
using Pokegraf.Application.Contract.Core.Client;
using Pokegraf.Application.Contract.Core.Context;
using Pokegraf.Common.ErrorHandling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static OperationResult.Helpers;
using static Pokegraf.Common.ErrorHandling.Helpers;

namespace Pokegraf.Application.Implementation.Core.Client
{
    public class ActionClient : IActionClient
    {
        protected readonly ILogger<ActionClient> Logger;
        protected readonly IStrategyContext StrategyContext;
        protected readonly IBotContext BotContext;

        public ActionClient(ILogger<ActionClient> logger, IStrategyContext strategyContext, IBotContext botContext)
        {
            Logger = logger;
            StrategyContext = strategyContext;
            BotContext = botContext;
        }
        
        public Result<ICommandAction, ResultError> GetCommandAction()
        {
            var command = GetCommand(BotContext.Message);
            if (command == null) return Error(NotFound("No corresponding action found."));
            
            var action = StrategyContext.GetCommandStrategyContext()
                .FirstOrDefault(botAction => botAction.CanHandle(command));
            
            if (action == null) return Error(NotFound("No corresponding action found."));

            return Ok(action);
        }
        
        public Result<IUpdateAction, ResultError> GetUpdateAction()
        {
            var updateType = GetUpdateType(BotContext.Update);
            if (updateType == null) return Error(NotFound("No corresponding action found."));

            var action = StrategyContext.GetUpdateStrategyContext()
                .FirstOrDefault(botAction => botAction.CanHandle(updateType));

            if (action == null) return Error(NotFound("No corresponding action found."));

            return Ok(action);
        }
        
        public Result<ICallbackAction, ResultError> GetCallbackAction()
        {
            var callback = GetCallback(BotContext.CallbackQuery);
            if (callback == null) return Error(NotFound("No corresponding action found."));

            var action = StrategyContext.GetCallbackStrategyContext()
                .FirstOrDefault(botAction => botAction.CanHandle(callback));
            
            if (action == null) return Error(NotFound("No corresponding action found."));

            return Ok(action);
        }

        public Result<IInlineAction, ResultError> GetInlineAction()
        {
            var inline = BotContext.InlineQuery.Query;
            
            if (inline == null) return Error(NotFound("No corresponding action found."));

            var action = StrategyContext.GetInlineStrategyContext()
                .FirstOrDefault(botAction => botAction.CanHandle(inline));
            
            if (action == null) return Error(NotFound("No corresponding action found."));

            return Ok(action);
        }

        private string GetUpdateType(Update update)
        {
            return update.Type == UpdateType.Message
                ? update.Message.Type.ToString() 
                : update.Type.ToString();
        }
        
        private string GetCommand(Message message)
        {
            if (message.Entities?.First()?.Type != MessageEntityType.BotCommand) return null;
            
            var command = message.EntityValues.First();

            if (!command.Contains('@')) return command;
                
            if (!command.Contains(BotContext.BotName)) return null;

            command = command.Substring(0, command.IndexOf('@'));

            return command;
        }

        private string GetCallback(CallbackQuery callbackQuery)
        {
            try
            {
                var callbackData = JsonConvert.DeserializeObject<Dictionary<string, string>>(callbackQuery.Data);

                return callbackData["action"];
            }
            catch
            {
                return null;
            }
        }
    }
}