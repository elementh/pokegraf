using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.WellKnownTypes;
using Pokegraf.Infrastructure.Contract.Dto;

namespace Pokegraf.Infrastructure.Implementation.Mapping.Extension
{
    public static class IntentExtension
    {
        public static IntentDto ToIntentDto(this DetectIntentResponse intentResponse)
        {
            var parameters = GetParameters(intentResponse.QueryResult.Parameters);
            
            return new IntentDto
            {
                DisplayName = intentResponse.QueryResult.Intent?.DisplayName,
                Parameters = parameters,
                Action = intentResponse.QueryResult.Action,
                FulfillmentText = intentResponse.QueryResult.FulfillmentText,
                IntentDetectionConfidence = intentResponse.QueryResult.IntentDetectionConfidence
            };
        }

        private static Dictionary<string, string> GetParameters(Struct queryResultParameters)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            
            foreach (var parameter in queryResultParameters.Fields)
            {
                var key = parameter.Key;

                var value = "";

                if (parameter.Value.KindCase == Value.KindOneofCase.StringValue)
                {
                    value = parameter.Value.StringValue;
                }

                if (parameter.Value.KindCase == Value.KindOneofCase.NumberValue)
                {
                    value = $"{parameter.Value.NumberValue:0}";
                }

                parameters.Add(key, value);
            }

            return parameters;
        }
    }
}