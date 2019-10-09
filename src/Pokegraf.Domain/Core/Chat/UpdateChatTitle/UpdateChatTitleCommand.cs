namespace Pokegraf.Domain.Core.Chat.UpdateChatTitle
{
    public class UpdateChatTitleCommand : Request<Result>
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