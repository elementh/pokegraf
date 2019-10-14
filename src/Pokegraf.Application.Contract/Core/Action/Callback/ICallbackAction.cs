using System.Collections.Generic;

namespace Pokegraf.Application.Contract.Core.Action.Callback
{
    public interface ICallbackAction : IBotAction
    {
        Dictionary<string, string> Data { get; set; }
    }
}