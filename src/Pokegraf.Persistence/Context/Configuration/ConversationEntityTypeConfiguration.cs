using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokegraf.Domain.Entity;

namespace Pokegraf.Persistence.Context.Configuration
{
    public class ConversationEntityTypeConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(e => new { e.ChatId, e.UserId });

            builder.HasOne(d => d.Chat)
                .WithMany(p => p.Conversations)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.User)
                .WithMany(p => p.Conversations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}