using MediatR;
using OperationResult;

namespace Pokegraf.Application.Contract.Core.Responses
{
    public interface IResponse : IRequest<Status>
    {
    }
}