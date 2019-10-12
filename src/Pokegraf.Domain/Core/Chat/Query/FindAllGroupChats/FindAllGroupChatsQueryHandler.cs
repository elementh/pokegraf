using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;

namespace Pokegraf.Domain.Core.Chat.Query.FindAllGroupChats
{
    internal class FindAllGroupChatsQueryHandler : CommonHandler<FindAllGroupChatsQuery, Result<IEnumerable<Entity.Chat>>>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public FindAllGroupChatsQueryHandler(ILogger<CommonHandler<FindAllGroupChatsQuery, Result<IEnumerable<Entity.Chat>>>> logger, IMediator mediatR, IUnitOfWork unitOfWork) : base(logger, mediatR)
        {
            UnitOfWork = unitOfWork;
        }

        public override async Task<Result<IEnumerable<Entity.Chat>>> Handle(FindAllGroupChatsQuery request, CancellationToken cancellationToken)
        {
            var groupChats = await UnitOfWork.ChatRepository.SearchBy(c => c.Type == Entity.Chat.ChatType.Group);
            
            return  Result<IEnumerable<Entity.Chat>>.Success(groupChats);
        }
    }
}