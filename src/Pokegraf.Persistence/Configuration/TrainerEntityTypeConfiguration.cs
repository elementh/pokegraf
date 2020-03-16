using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Context.Configuration;
using Pokegraf.Core.Entity;

namespace Pokegraf.Persistence.Configuration
{
    public class TrainerEntityTypeConfiguration : UserEntityTypeConfiguration<Trainer>
    {
        public override void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.HasOne(e => e.Stats)
                .WithOne(e => e.Trainer)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}