using System.Collections.Generic;

namespace Pokegraf.Application.Contract.Model
{
    public class Intent
    {
        public string Action { get; set; }
        public string DisplayName { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string FulfillmentText { get; set; }
        public float IntentDetectionConfidence { get; set; }
    }
}