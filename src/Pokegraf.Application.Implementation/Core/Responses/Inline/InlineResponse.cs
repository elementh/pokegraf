using Pokegraf.Application.Contract.Core.Responses;
using Telegram.Bot.Types.InlineQueryResults;

namespace Pokegraf.Application.Implementation.Core.Responses.Inline
{
    public class InlineResponse : IResponse
    {
        public InlineQueryResultBase[] Results { get; set; }

        public InlineResponse(InlineQueryResultBase[] results)
        {
            Results = results;
        }
    }
}