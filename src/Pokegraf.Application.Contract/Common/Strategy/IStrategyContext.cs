using System.Collections.Generic;
using Pokegraf.Application.Contract.Model.Action;
using Pokegraf.Application.Contract.Model.Action.Callback;
using Pokegraf.Application.Contract.Model.Action.Command;
using Pokegraf.Application.Contract.Model.Action.Conversation;
using Pokegraf.Application.Contract.Model.Action.Inline;

namespace Pokegraf.Application.Contract.Common.Strategy
{
    public interface IStrategyContext
    {
        IEnumerable<IConversationAction> GetConversationStrategyContext();
        IEnumerable<ICallbackAction> GetCallbackStrategyContext();
        IEnumerable<ICommandAction> GetCommandStrategyContext();
        IEnumerable<IInlineAction> GetInlineStrategyContext();
    }
}