using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Domain.Entities;

namespace MusicStore.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);
            builder.Property(o => o.Username).IsRequired().HasMaxLength(256);
            builder.Property(o => o.FirstName).IsRequired().HasMaxLength(160);
            builder.Property(o => o.LastName).IsRequired().HasMaxLength(160);
            builder.Property(o => o.Address).IsRequired().HasMaxLength(70);
            builder.Property(o => o.City).IsRequired().HasMaxLength(40);
            builder.Property(o => o.State).IsRequired().HasMaxLength(40);
            builder.Property(o => o.PostalCode).IsRequired().HasMaxLength(10);
            builder.Property(o => o.Country).IsRequired().HasMaxLength(40);
            builder.Property(o => o.Phone).IsRequired().HasMaxLength(24);
            builder.Property(o => o.Email).IsRequired().HasMaxLength(160);
            builder.Property(o => o.Total).HasColumnType("decimal(18,2)");
        }
    }
}
