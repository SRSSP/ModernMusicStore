using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Domain.Entities
{
    public class Album
    {
        public int AlbumId { get; set; }
        [Required]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Please select a genre.")]
        public int? GenreId { get; set; }

        [Required(ErrorMessage = "Please select an artist.")]
        public int? ArtistId { get; set; }
        public decimal Price { get; set; }
        public string? AlbumArtUrl { get; set; }

        // Navigation properties
        // Make navigation properties nullable so model validation focuses on the Id fields
        public Genre? Genre { get; set; }
        public Artist? Artist { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
