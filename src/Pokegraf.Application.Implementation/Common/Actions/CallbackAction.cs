using System.Collections.Generic;
using Pokegraf.Application.Contract.Common.Actions;

namespace Pokegraf.Application.Implementation.Common.Actions
{
    public abstract class CallbackAction : BotAction, ICallbackAction
    {
        public Dictionary<string, string> Data { get; set; }
    }
}