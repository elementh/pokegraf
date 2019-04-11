using System.Collections.Specialized;

namespace Pokegraf.Application.Contract.BotActions.Common
{
    public interface ICallbackAction : IBotAction
    {
        OrderedDictionary Data { get; set; }
    }
}