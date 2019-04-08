using Telegram.Bot.Types;

namespace Pokegraf.Application.Contract.BotAction.Common
{
    public interface IBotActionFactory
    {
        IBotAction GetBotAction(Message message);
    }
}