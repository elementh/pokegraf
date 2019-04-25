using Google.Cloud.Dialogflow.V2;
using Pokegraf.Infrastructure.Contract.Model;

namespace Pokegraf.Infrastructure.Implementation.Mapping.Profile
{
    public class DetectIntentQueryProfile : AutoMapper.Profile
    {
        DetectIntentQueryProfile()
        {
            CreateMap<DetectIntentQuery, TextInput>();
        }
    }
}