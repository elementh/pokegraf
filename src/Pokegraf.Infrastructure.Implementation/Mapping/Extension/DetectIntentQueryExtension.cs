using AutoMapper;
using Google.Cloud.Dialogflow.V2;
using Pokegraf.Infrastructure.Contract.Model;

namespace Pokegraf.Infrastructure.Implementation.Mapping.Extension
{
    public static class DetectIntentQueryExtension
    {
        internal static IMapper Mapper { get; }
        
        static DetectIntentQueryExtension()
        {
            Mapper = new MapperConfiguration(cfg => { }).CreateMapper();
        }

        public static QueryInput ToQueryInput(this DetectIntentQuery query)
        {
            return new QueryInput
            {
                Text = query.ToTextInput()
            };
        }

        public static TextInput ToTextInput(this DetectIntentQuery query)
        {
            return query != null
                ? Mapper.Map<TextInput>(query)
                : null;
        }
    }
}