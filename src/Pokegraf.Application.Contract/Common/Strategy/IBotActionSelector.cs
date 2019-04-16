using Pokegraf.Application.Contract.BotActions.Common;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Contract.Common.Strategy
{
    public interface IBotActionSelector
    {
        Result<ICommandAction> GetCommandAction();
        Result<ICallbackAction> GetCallbackAction();
        Result<IInlineAction> GetInlineAction();
    }
}