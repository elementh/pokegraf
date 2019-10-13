using MediatR;

namespace Pokegraf.Domain.Core.Chat.Command.UpdateChatTitle
{
    public class UpdateChatTitleCommand : IRequest
    {
        /// <summary>
        /// Id of the chat.
        /// </summary>
        public long ChatId { get; set; }
        /// <summary>
        /// New title of the chat.
        /// </summary>
        public string Title { get; set; }
    }
}