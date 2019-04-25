using System.Collections.Generic;

namespace Pokegraf.Infrastructure.Contract.Dto
{
    public class IntentDto
    {
        public string Name { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string FulfillmentText { get; set; }
        public float IntentDetectionConfidence { get; set; }
    }
}