using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokegraf.Domain.Entity;

namespace Pokegraf.Persistence.Implementation.Context.Configuration
{
    public class StatsEntityTypeConfiguration : IEntityTypeConfiguration<Stats>
    {
        public void Configure(EntityTypeBuilder<Stats> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.OwnsOne(e => e.Requests, od =>
            {
                od.ToTable("StatsRequests");
            });
        }
    }
}