using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokegraf.Domain.Entity;

namespace Pokegraf.Persistence.Context.Configuration
{
    public class ChatEntityTypeConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();
        }
    }
}