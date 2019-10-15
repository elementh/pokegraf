using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokegraf.Domain.Entity;

namespace Pokegraf.Persistence.Implementation.Context.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();
            
            builder.HasOne(d => d.Stats)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}