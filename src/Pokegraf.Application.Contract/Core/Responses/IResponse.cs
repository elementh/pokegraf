using MediatR;
using OperationResult;
using Pokegraf.Common.ErrorHandling;

namespace Pokegraf.Application.Contract.Core.Responses
{
    public interface IResponse : IRequest<Status<Error>>
    {
    }
}