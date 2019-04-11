using System.Collections.Generic;
using System.Collections.Specialized;

namespace Pokegraf.Application.Contract.BotActions.Common
{
    public interface ICallbackAction : IBotAction
    {
        Dictionary<string, string> Data { get; set; }
    }
}