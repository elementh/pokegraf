using System.Linq;
using Google.Cloud.Dialogflow.V2;
using Pokegraf.Infrastructure.Contract.Dto;

namespace Pokegraf.Infrastructure.Implementation.Mapping.Extension
{
    public static class IntentExtension
    {
        public static IntentDto ToIntentDto(this DetectIntentResponse intentResponse)
        {
            return new IntentDto
            {
                DisplayName = intentResponse.QueryResult.Intent?.DisplayName,
                Parameters = intentResponse.QueryResult.Parameters.Fields.ToDictionary(pair => pair.Key, pair => pair.Value.ToString()),
                Action = intentResponse.QueryResult.Action,
                FulfillmentText = intentResponse.QueryResult.FulfillmentText,
                IntentDetectionConfidence = intentResponse.QueryResult.IntentDetectionConfidence
            };
        }
    }
}