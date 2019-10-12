using MediatR;
using OperationResult;

namespace Pokegraf.Application.Contract.Common.Responses
{
    public interface IResponse : IRequest<Status>
    {
    }
}