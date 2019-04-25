using System.Threading.Tasks;
using Pokegraf.Application.Contract.Model.Action;
using Pokegraf.Application.Contract.Model.Action.Callback;
using Pokegraf.Application.Contract.Model.Action.Command;
using Pokegraf.Application.Contract.Model.Action.Inline;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Contract.Common.Strategy
{
    public interface IBotActionSelector
    {
        Task<Result<ICommandAction>> GetCommandAction();
        Result<ICallbackAction> GetCallbackAction();
        Result<IInlineAction> GetInlineAction();
    }
}