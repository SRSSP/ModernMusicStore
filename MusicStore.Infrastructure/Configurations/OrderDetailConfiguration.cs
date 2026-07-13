using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Domain.Entities;

namespace MusicStore.Infrastructure.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => od.OrderDetailId);
            builder.Property(od => od.UnitPrice).HasColumnType("decimal(18,2)");
            builder.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);
            builder.HasOne(od => od.Album)
                .WithMany(a => a.OrderDetails)
                .HasForeignKey(od => od.AlbumId);
        }
    }
}
