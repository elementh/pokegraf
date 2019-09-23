using Pokegraf.Application.Contract.Model.Action.Callback;
using Pokegraf.Application.Contract.Model.Action.Command;
using Pokegraf.Application.Contract.Model.Action.Inline;
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