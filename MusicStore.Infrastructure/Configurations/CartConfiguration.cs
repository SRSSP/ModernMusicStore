using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Domain.Entities;

namespace MusicStore.Infrastructure.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.RecordId);
            builder.Property(c => c.CartId).IsRequired().HasMaxLength(100);
            builder.HasOne(c => c.Album)
                .WithMany()
                .HasForeignKey(c => c.AlbumId);
        }
    }
}
