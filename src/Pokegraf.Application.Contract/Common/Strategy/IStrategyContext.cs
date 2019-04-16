using System.Collections.Generic;
using Pokegraf.Application.Contract.BotActions.Common;
using Pokegraf.Application.Contract.Common.Actions;

namespace Pokegraf.Application.Contract.Common.Strategy
{
    public interface IStrategyContext
    {
        IEnumerable<ICallbackAction> GetCallbackStrategyContext();
        IEnumerable<ICommandAction> GetCommandStrategyContext();
        IEnumerable<IInlineAction> GetInlineStrategyContext();
    }
}