using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Application.Interfaces;
using MusicStore.Domain.Entities;
using MusicStore.Infrastructure.Repositories;

namespace MusicStore.Application.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public Task<IEnumerable<Album>> GetAlbumsAsync()
            => _albumRepository.GetAllAsync();

        public Task<Album?> GetAlbumByIdAsync(int albumId)
            => _albumRepository.GetByIdAsync(albumId);

        public Task<IEnumerable<Album>> GetAlbumsByGenreAsync(int genreId)
            => _albumRepository.GetByGenreAsync(genreId);

        public async Task AddAlbumAsync(Album album)
        {
            await _albumRepository.AddAsync(album);
        }

        public async Task UpdateAlbumAsync(Album album)
        {
            await _albumRepository.UpdateAsync(album);
        }

        public async Task DeleteAlbumAsync(int albumId)
        {
            var album = await _albumRepository.GetByIdAsync(albumId);
            if (album != null)
            {
                await _albumRepository.DeleteAsync(album);
            }
        }
    }
}
