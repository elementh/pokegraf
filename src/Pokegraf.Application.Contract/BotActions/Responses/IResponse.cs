using MediatR;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Contract.BotActions.Responses
{
    public interface IResponse : IRequest<Result>
    {
        long ChatId { get; set; }
    }
}