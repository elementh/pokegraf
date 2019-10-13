namespace Pokegraf.Application.Implementation.Core.Responses.Photo
{
    public class PhotoWithCaptionResponse : PhotoResponse
    {
        public string Caption { get; set; }

        public PhotoWithCaptionResponse(string photo, string caption) : base(photo)
        {
            Caption = caption;
        }
    }
}