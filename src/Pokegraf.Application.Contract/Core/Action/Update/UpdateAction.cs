using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Contract.Core.Action.Update
{
    public abstract class UpdateAction : BotAction, IUpdateAction
    {
        public Telegram.Bot.Types.Update Update { get; set; }
        public UpdateAction(IBotContext botContext) : base(botContext)
        {
            Update = botContext.Update;
        }

    }
}