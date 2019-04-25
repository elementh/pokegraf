using System.Collections.Generic;
using Pokegraf.Application.Contract.Common.Strategy;
using Pokegraf.Application.Contract.Model.Action;
using Pokegraf.Application.Contract.Model.Action.Callback;
using Pokegraf.Application.Contract.Model.Action.Command;
using Pokegraf.Application.Contract.Model.Action.Conversation;
using Pokegraf.Application.Contract.Model.Action.Inline;

namespace Pokegraf.Application.Implementation.Common.Strategy
{
    public class StrategyContext : IStrategyContext
    {
        protected IEnumerable<IConversationAction> ConversationActions;
        protected IEnumerable<ICallbackAction> CallbackActions;
        protected IEnumerable<ICommandAction> CommandActions;
        protected IEnumerable<IInlineAction> InlineActions;

        public StrategyContext(IEnumerable<IConversationAction> conversationActions, IEnumerable<ICallbackAction> callbackActions, IEnumerable<ICommandAction> commandActions, IEnumerable<IInlineAction> inlineActions)
        {
            ConversationActions = conversationActions;
            CallbackActions = callbackActions;
            CommandActions = commandActions;
            InlineActions = inlineActions;
        }

        public IEnumerable<IConversationAction> GetConversationStrategyContext()
        {
            return ConversationActions;
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