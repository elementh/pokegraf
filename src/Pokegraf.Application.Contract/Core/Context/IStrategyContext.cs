using System.Collections.Generic;
using Pokegraf.Application.Contract.Action.Callback;
using Pokegraf.Application.Contract.Action.Command;
using Pokegraf.Application.Contract.Action.Inline;
using Pokegraf.Application.Contract.Action.Update;

namespace Pokegraf.Application.Contract.Core.Context
{
    public interface IStrategyContext
    {
        IEnumerable<ICallbackAction> GetCallbackStrategyContext();
        IEnumerable<ICommandAction> GetCommandStrategyContext();
        IEnumerable<IInlineAction> GetInlineStrategyContext();
        IEnumerable<IUpdateAction> GetUpdateStrategyContext();
    }
}