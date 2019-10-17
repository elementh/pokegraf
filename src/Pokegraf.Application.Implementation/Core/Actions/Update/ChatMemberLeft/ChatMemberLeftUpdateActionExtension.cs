using Pokegraf.Domain.Conversation.Command.DeleteConversation;

namespace Pokegraf.Application.Implementation.Core.Actions.Update.ChatMemberLeft
{
    public static class ChatMemberLeftUpdateActionExtension
    {
        public static DeleteConversationCommand MapToDeleteConversationCommand(this ChatMemberLeftUpdateAction chatMemberLeftUpdateAction)
        {
            return new DeleteConversationCommand
            {
                ChatId = chatMemberLeftUpdateAction.Chat.Id,
                UserId = chatMemberLeftUpdateAction.From.Id
            };
        }
    }
}