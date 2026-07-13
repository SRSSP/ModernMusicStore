using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Domain.Entities;

namespace MusicStore.Infrastructure.Configurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(a => a.AlbumId);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(160);
            builder.Property(a => a.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(a => a.Genre)
                .WithMany(g => g.Albums)
                .HasForeignKey(a => a.GenreId);
            builder.HasOne(a => a.Artist)
                .WithMany(ar => ar.Albums)
                .HasForeignKey(a => a.ArtistId);
        }
    }
}
