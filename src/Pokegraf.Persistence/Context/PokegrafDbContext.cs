using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Context;
using Navigator.Extensions.Store.Entity;
using Pokegraf.Core.Entity;
using Pokegraf.Persistence.Configuration;

namespace Pokegraf.Persistence.Context
{
    public class PokegrafDbContext : NavigatorDbContext<Trainer, Chat>
    {
        public DbSet<Stats> Stats { get; set; }
        
        public PokegrafDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new TrainerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StatsEntityTypeConfiguration());
        }
    }
}