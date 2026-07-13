using System.Collections.Generic;

namespace MusicStore.Domain.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        // Navigation property
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
