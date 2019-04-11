using System.Collections.Specialized;
using Pokegraf.Application.Contract.BotActions.Common;

namespace Pokegraf.Application.Implementation.BotActions.Common
{
    public class CallbackAction : BotAction, ICallbackAction
    {
        public OrderedDictionary Data { get; set; }
    }
}