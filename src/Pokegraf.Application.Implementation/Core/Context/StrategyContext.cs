using System.Collections.Generic;
using Pokegraf.Application.Contract.Action.Callback;
using Pokegraf.Application.Contract.Action.Command;
using Pokegraf.Application.Contract.Action.Inline;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Context
{
    public class StrategyContext : IStrategyContext
    {
        protected IEnumerable<ICallbackAction> CallbackActions;
        protected IEnumerable<ICommandAction> CommandActions;
        protected IEnumerable<IInlineAction> InlineActions;

        public StrategyContext(IEnumerable<ICallbackAction> callbackActions, IEnumerable<ICommandAction> commandActions, IEnumerable<IInlineAction> inlineActions)
        {
            CallbackActions = callbackActions;
            CommandActions = commandActions;
            InlineActions = inlineActions;
        }
        
        public IEnumerable<ICallbackAction> GetCallbackStrategyContext()
        {
            return CallbackActions;
        }

        public IEnumerable<ICommandAction> GetCommandStrategyContext()
        {
            return CommandActions;
        }

        public IEnumerable<IInlineAction> GetInlineStrategyContext()
        {
            return InlineActions;
        }
    }
}