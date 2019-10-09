using Microsoft.EntityFrameworkCore;

namespace Pokegraf.Persistence.Contract.Context
{
    public interface IDbContext
    {
        DbContext Instance { get; }
    }
}