using System.Collections.Generic;

namespace MusicStore.Domain.Entities
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; } = null!;
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public decimal Price { get; set; }
        public string? AlbumArtUrl { get; set; }

        // Navigation properties
        public Genre Genre { get; set; } = null!;
        public Artist Artist { get; set; } = null!;
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
