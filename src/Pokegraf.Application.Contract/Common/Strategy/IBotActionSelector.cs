using OperationResult;
using Pokegraf.Application.Contract.Model.Action.Callback;
using Pokegraf.Application.Contract.Model.Action.Command;
using Pokegraf.Application.Contract.Model.Action.Inline;
using Pokegraf.Common.ErrorHandling;

namespace Pokegraf.Application.Contract.Common.Strategy
{
    public interface IBotActionSelector
    {
        Result<ICommandAction, ResultError> GetCommandAction();
        Result<ICallbackAction, ResultError> GetCallbackAction();
        Result<IInlineAction, ResultError> GetInlineAction();
    }
}