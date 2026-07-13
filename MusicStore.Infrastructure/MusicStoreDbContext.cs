using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.Entities;
using MusicStore.Infrastructure.Configurations;

namespace MusicStore.Infrastructure
{
    // Use IdentityDbContext so ASP.NET Core Identity tables are created and managed
    public class MusicStoreDbContext : IdentityDbContext<IdentityUser>
    {
        public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Let Identity configure its schema first
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
        }
    }
}
