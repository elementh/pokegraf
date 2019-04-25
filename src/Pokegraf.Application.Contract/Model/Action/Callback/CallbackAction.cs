using System.Collections.Generic;
using Newtonsoft.Json;
using Pokegraf.Application.Contract.Common.Context;

namespace Pokegraf.Application.Contract.Model.Action.Callback
{
    public abstract class CallbackAction : BotAction, ICallbackAction
    {
        public Dictionary<string, string> Data { get; set; }

        public CallbackAction(IBotContext botContext) : base(botContext)
        {
            Dictionary<string, string> callbackData;
            try
            {
                callbackData = JsonConvert.DeserializeObject<Dictionary<string, string>>(botContext.CallbackQuery.Data);
            }
            catch
            {
                callbackData = new Dictionary<string, string>();
            }

            Data = callbackData;
        }
    }
}