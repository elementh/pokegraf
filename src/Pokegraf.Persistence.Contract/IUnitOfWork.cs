using System.Threading;
using System.Threading.Tasks;
using Pokegraf.Persistence.Contract.Repository;

namespace Pokegraf.Persistence.Contract
{
    public interface IUnitOfWork
    {
        IChatRepository ChatRepository { get; set; }
        IConversationRepository ConversationRepository { get; set; }
        IUserRepository UserRepository { get; set; }
        IStatsRepository StatsRepository { get; set; }
        
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}