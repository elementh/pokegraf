using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Core.Chat.FindAllPrivateChats
{
    internal class FindAllPrivateChatsQueryHandler : CommonHandler<FindAllPrivateChatsQuery, Result<IEnumerable<Entity.Chat>>>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public FindAllPrivateChatsQueryHandler(ILogger<CommonHandler<FindAllPrivateChatsQuery, Result<IEnumerable<Entity.Chat>>>> logger, IMediator mediatR, IUnitOfWork unitOfWork) : base(logger, mediatR)
        {
            UnitOfWork = unitOfWork;
        }

        public override async Task<Result<IEnumerable<Entity.Chat>>> Handle(FindAllPrivateChatsQuery request, CancellationToken cancellationToken)
        {
            var privateChats = await UnitOfWork.ChatRepository.SearchBy(c => c.Type == Entity.Chat.ChatType.Private);
            
            return  Result<IEnumerable<Entity.Chat>>.Success(privateChats);
        }
    }
}