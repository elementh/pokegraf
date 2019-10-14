using OperationResult;
using Pokegraf.Application.Contract.Action.Callback;
using Pokegraf.Application.Contract.Action.Command;
using Pokegraf.Application.Contract.Action.Inline;
using Pokegraf.Common.ErrorHandling;

namespace Pokegraf.Application.Contract.Core.Client
{
    public interface IActionClient
    {
        Result<ICommandAction, ResultError> GetCommandAction();
        Result<ICallbackAction, ResultError> GetCallbackAction();
        Result<IInlineAction, ResultError> GetInlineAction();
    }
}