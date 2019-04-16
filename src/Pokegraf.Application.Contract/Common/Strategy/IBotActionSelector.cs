using Pokegraf.Application.Contract.Common.Actions;
using Pokegraf.Common.Result;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Contract.BotActions.Common
{
    public interface IBotActionSelector
    {
        Result<ICommandAction> GetCommandAction();
        Result<ICallbackAction> GetCallbackAction();
        Result<IInlineAction> GetInlineAction();
    }
}