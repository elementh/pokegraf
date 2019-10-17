using Pokegraf.Application.Contract.Core.Context;

namespace Pokegraf.Application.Contract.Core.Action.Inline
{
    public abstract class InlineAction : BotAction, IInlineAction
    {
        public string Query { get; set; }
        public string Offset { get; set; }
     
        public InlineAction(IBotContext botContext) : base(botContext)
        {
            Query = botContext.InlineQuery?.Query;
            Offset = botContext.InlineQuery?.Offset;
        }
    }
}