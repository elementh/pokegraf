using System;
using Microsoft.EntityFrameworkCore;
using Pokegraf.Domain.Entity;
using Pokegraf.Persistence.Contract.Context;

namespace Pokegraf.Persistence.Implementation.Context
{
    public class PokegrafDbContext : DbContext, IPokegrafDbContext
    {
        public DbContext Instance => this;

        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Stats> Stats { get; set; }
        
        protected PokegrafDbContext()
        {
        }

        public PokegrafDbContext(DbContextOptions<PokegrafDbContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("Database not properly configured");
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.HasKey(e => new { e.ChatId, e.UserId });

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.Conversations)
                    .HasForeignKey(d => d.ChatId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Conversations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokegrafDbContext).Assembly);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}