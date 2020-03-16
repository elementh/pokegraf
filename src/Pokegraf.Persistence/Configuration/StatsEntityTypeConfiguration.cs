using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokegraf.Core.Entity;

namespace Pokegraf.Persistence.Configuration
{
    public class StatsEntityTypeConfiguration : IEntityTypeConfiguration<Stats>
    {
        public void Configure(EntityTypeBuilder<Stats> builder)
        {
            builder.OwnsOne(e => e.Requests, od =>
            {
                od.ToTable("StatsRequests");
            });
        }
    }
}