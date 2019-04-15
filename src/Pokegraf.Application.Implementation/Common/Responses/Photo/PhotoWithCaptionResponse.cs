namespace Pokegraf.Application.Implementation.Common.Responses.Photo
{
    public class PhotoWithCaptionResponse : PhotoResponse
    {
        public string Caption { get; set; }

        public PhotoWithCaptionResponse(long chatId, string photo, string caption) : base(chatId, photo)
        {
            Caption = caption;
        }
    }
}