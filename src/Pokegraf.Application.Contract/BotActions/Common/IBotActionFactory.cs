using Pokegraf.Common.Result;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Contract.BotActions.Common
{
    public interface IBotActionFactory
    {
        Result<IBotAction> GetBotAction(Message message);
        Result<ICallbackAction> GetCallbackAction(CallbackQuery callbackQuery);
        Result<IInlineAction> GetInlineAction(InlineQuery eInlineQuery);
    }
}