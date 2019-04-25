using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action.Conversation;

namespace Pokegraf.Application.Implementation.BotActions.Conversation.PokemonStats
{
    public class PokemonStatsConversationAction : ConversationAction
    {
        protected override string Action { get; }

        public PokemonStatsConversationAction(IBotContext botContext) : base(botContext)
        {
            Action = "pokemon.stats";
        }
    }
}