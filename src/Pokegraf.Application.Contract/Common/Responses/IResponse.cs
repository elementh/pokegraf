using MediatR;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Contract.Common.Responses
{
    public interface IResponse : IRequest<Result>
    {
    }
}