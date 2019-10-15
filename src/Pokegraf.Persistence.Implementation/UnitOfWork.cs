using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pokegraf.Persistence.Contract;
using Pokegraf.Persistence.Contract.Context;
using Pokegraf.Persistence.Contract.Repository;

namespace Pokegraf.Persistence.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly IPokegrafDbContext Context;
        protected readonly ILogger<UnitOfWork> Logger;
        private bool _disposed;

        public IChatRepository ChatRepository { get; set; }
        public IConversationRepository ConversationRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IStatsRepository StatsRepository { get; set; }

        public UnitOfWork(IPokegrafDbContext context, ILogger<UnitOfWork> logger, IChatRepository chatRepository, IConversationRepository conversationRepository, IUserRepository userRepository, IStatsRepository statsRepository)
        {
            Context = context;
            Logger = logger;
            ChatRepository = chatRepository;
            ConversationRepository = conversationRepository;
            UserRepository = userRepository;
            StatsRepository = statsRepository;
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await Context.Instance.SaveChangesAsync(cancellationToken);
                
                Logger.LogInformation("Database save operation finished correctly.");
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.LogError(e, "Database save operation failed, concurrency related.");

                throw;
            }
            catch (DbUpdateException e)
            {
                Logger.LogError(e, "Database save operation failed.");

                throw;
            }
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Instance.Dispose();
                }
            }
            
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}