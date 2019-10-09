using Microsoft.EntityFrameworkCore;
using Pokegraf.Domain.Entity;

namespace Pokegraf.Persistence.Contract.Context
{
    public interface IPokegrafDbContext : IDbContext
    {
        DbSet<Chat> Chats { get; set; }
       
        DbSet<Conversation> Conversations { get; set; }
       
        DbSet<User> Users { get; set; }
    }
}