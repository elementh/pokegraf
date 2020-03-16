using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pokegraf.Core.Domain.Resources
{
    public static class CallbackActions
    {
        public static string FusionAction => JsonConvert.SerializeObject(new Dictionary<string, string> {{"action", "fusion"}});
    }
}