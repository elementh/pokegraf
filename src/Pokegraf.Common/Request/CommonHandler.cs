using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Pokegraf.Common.Request
{
    public abstract class CommonHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : Request<TResponse>
    {
        protected readonly ILogger<CommonHandler<TRequest, TResponse>> Logger;
        protected readonly IMediator MediatR;
        
        protected CommonHandler(ILogger<CommonHandler<TRequest, TResponse>> logger, IMediator mediatR)
        {
            Logger = logger;
            MediatR = mediatR;
        }
    
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}