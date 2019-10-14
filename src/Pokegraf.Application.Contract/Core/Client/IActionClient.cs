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
        Result<ICommandAction, Error> GetCommandAction();
        Result<ICallbackAction, Error> GetCallbackAction();
        Result<IInlineAction, Error> GetInlineAction();
        Result<IUpdateAction, Error> GetUpdateAction();

    }
}