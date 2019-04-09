using Pokegraf.Common.Result;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Contract.BotAction.Common
{
    public interface IBotActionFactory
    {
        Result<IBotAction> GetBotAction(Message message);
    }
}