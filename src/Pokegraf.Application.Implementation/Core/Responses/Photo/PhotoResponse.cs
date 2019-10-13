using Pokegraf.Application.Contract.Core.Responses;

namespace Pokegraf.Application.Implementation.Core.Responses.Photo
{
    public class PhotoResponse : Request<Result>, IResponse
    {
        public string Photo { get; set; }

        public PhotoResponse(string photo)
        {
            Photo = photo;
        }
    }
}