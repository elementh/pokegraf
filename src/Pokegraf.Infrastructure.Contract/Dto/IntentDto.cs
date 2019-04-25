using System.Collections.Generic;

namespace Pokegraf.Infrastructure.Contract.Dto
{
    public class IntentDto
    {
        public string Action { get; set; }
        public string DisplayName { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string FulfillmentText { get; set; }
        public float IntentDetectionConfidence { get; set; }
    }
}