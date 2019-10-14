using System.Collections.Generic;
using Pokegraf.Application.Contract.Core.Action.Callback;
using Pokegraf.Application.Contract.Core.Action.Command;
using Pokegraf.Application.Contract.Core.Action.Inline;
using Pokegraf.Application.Contract.Core.Action.Update;
using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Implementation.Core.Context
{
    public class StrategyContext : IStrategyContext
    {
        protected IEnumerable<ICallbackAction> CallbackActions;
        protected IEnumerable<ICommandAction> CommandActions;
        protected IEnumerable<IInlineAction> InlineActions;
        protected IEnumerable<IUpdateAction> UpdateActions;

        public StrategyContext(IEnumerable<ICallbackAction> callbackActions, IEnumerable<ICommandAction> commandActions, IEnumerable<IInlineAction> inlineActions, IEnumerable<IUpdateAction> updateActions)
        {
            CallbackActions = callbackActions;
            CommandActions = commandActions;
            InlineActions = inlineActions;
            UpdateActions = updateActions;
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

        public IEnumerable<IUpdateAction> GetUpdateStrategyContext()
        {
            return UpdateActions;
        }
    }
}