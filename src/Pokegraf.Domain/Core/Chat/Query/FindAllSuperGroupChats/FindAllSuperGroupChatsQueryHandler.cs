using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Core.Chat.Query.FindAllSuperGroupChats
{
    internal class FindAllSuperGroupChatsQueryHandler : CommonHandler<FindAllSuperGroupChatsQuery, Result<IEnumerable<Entity.Chat>>>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public FindAllSuperGroupChatsQueryHandler(ILogger<CommonHandler<FindAllSuperGroupChatsQuery, Result<IEnumerable<Entity.Chat>>>> logger, IMediator mediatR, IUnitOfWork unitOfWork) : base(logger, mediatR)
        {
            UnitOfWork = unitOfWork;
        }

        public override async Task<Result<IEnumerable<Entity.Chat>>> Handle(FindAllSuperGroupChatsQuery request, CancellationToken cancellationToken)
        {
            var superGroupChats = await UnitOfWork.ChatRepository.SearchBy(c => c.Type == Entity.Chat.ChatType.Supergroup);
            
            return  Result<IEnumerable<Entity.Chat>>.Success(superGroupChats);
        }
    }
}