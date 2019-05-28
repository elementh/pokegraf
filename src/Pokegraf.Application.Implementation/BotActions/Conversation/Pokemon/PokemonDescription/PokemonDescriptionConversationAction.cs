using Pokegraf.Application.Contract.Common.Context;
using Pokegraf.Application.Contract.Model.Action.Conversation;

namespace Pokegraf.Application.Implementation.BotActions.Conversation.Pokemon.PokemonDescription
{
    public class PokemonDescriptionConversationAction : ConversationAction
    {
        protected override string Action { get; }

        public PokemonDescriptionConversationAction(IBotContext botContext) : base(botContext)
        {
            Action = "pokemon.description";
        }
    }
}