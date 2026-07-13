using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Domain.Entities;

namespace MusicStore.Application.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<Album>> GetAlbumsAsync();
        Task<Album?> GetAlbumByIdAsync(int albumId);
        Task<IEnumerable<Album>> GetAlbumsByGenreAsync(int genreId);
        Task AddAlbumAsync(Album album);
        Task UpdateAlbumAsync(Album album);
        Task DeleteAlbumAsync(int albumId);
    }
}
