using Pokegraf.Application.Contract.BotAction.Common;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.BotAction.Common
{
    public class BotActionFactory : IBotActionFactory
    {
        public IBotAction GetBotAction(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}