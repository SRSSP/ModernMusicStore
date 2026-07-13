using System.Collections.Generic;

namespace MusicStore.Domain.Entities
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; } = null!;

        // Navigation property
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
