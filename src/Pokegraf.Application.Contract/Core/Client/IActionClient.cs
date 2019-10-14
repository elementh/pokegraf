using OperationResult;
using Pokegraf.Application.Contract.Core.Action.Callback;
using Pokegraf.Application.Contract.Core.Action.Command;
using Pokegraf.Application.Contract.Core.Action.Inline;
using Pokegraf.Application.Contract.Core.Action.Update;
using Pokegraf.Common.ErrorHandling;

namespace Pokegraf.Application.Contract.Core.Client
{
    public interface IActionClient
    {
        Result<ICommandAction, ResultError> GetCommandAction();
        Result<ICallbackAction, ResultError> GetCallbackAction();
        Result<IInlineAction, ResultError> GetInlineAction();
        Result<IUpdateAction, ResultError> GetUpdateAction();

    }
}