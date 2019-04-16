using Pokegraf.Application.Contract.BotActions.Common;
using Pokegraf.Application.Contract.Common.Context;

namespace Pokegraf.Application.Implementation.BotActions.Common
{
    public abstract class InlineAction : BotAction, IInlineAction
    {
        public string Query { get; set; }
        public string Offset { get; set; }
     
        public InlineAction(IBotContext botContext) : base(botContext)
        {
            Query = botContext.InlineQuery.Query;
            Offset = botContext.InlineQuery.Offset;
        }
    }
}