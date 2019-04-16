using Pokegraf.Application.Contract.Common.Responses;
using Pokegraf.Common.Request;
using Pokegraf.Common.Result;
using Telegram.Bot.Types.InlineQueryResults;

namespace Pokegraf.Application.Implementation.Common.Responses.Inline
{
    public class InlineResponse : Request<Result>, IResponse
    {
        public InlineQueryResultBase[] Results { get; set; }

        public InlineResponse(InlineQueryResultBase[] results)
        {
            Results = results;
        }
    }
}