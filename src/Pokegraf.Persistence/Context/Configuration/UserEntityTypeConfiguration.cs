using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokegraf.Domain.Entity;

namespace Pokegraf.Persistence.Context.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();
            
            builder.HasOne(d => d.Stats)
                .WithOne(p => p.User)
                .HasForeignKey<User>(d => d.StatsId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}