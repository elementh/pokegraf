using System.Collections.Generic;

namespace Pokegraf.Application.Contract.Action.Callback
{
    public interface ICallbackAction : IBotAction
    {
        Dictionary<string, string> Data { get; set; }
    }
}