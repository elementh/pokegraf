using Pokegraf.Application.Contract.Core.Responses;

namespace Pokegraf.Application.Implementation.Core.Responses.Photo
{
    public class PhotoResponse : IResponse
    {
        public string Photo { get; set; }
        public string Caption { get; set; }

        public PhotoResponse(string photo, string caption)
        {
            Photo = photo;
            Caption = caption;
        }
    }
}