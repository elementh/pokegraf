using System.Collections.Generic;

namespace Pokegraf.Application.Contract.Common.Actions
{
    public interface ICallbackAction : IBotAction
    {
        Dictionary<string, string> Data { get; set; }
    }
}