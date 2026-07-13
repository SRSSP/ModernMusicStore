using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Domain.Entities;

namespace MusicStore.Application.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetArtistsAsync();
        Task<Artist?> GetArtistByIdAsync(int artistId);
    }
}
