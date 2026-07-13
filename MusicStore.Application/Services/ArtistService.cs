using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Application.Interfaces;
using MusicStore.Domain.Entities;
using MusicStore.Infrastructure.Repositories;

namespace MusicStore.Application.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public Task<IEnumerable<Artist>> GetArtistsAsync()
            => _artistRepository.GetAllAsync();

        public Task<Artist?> GetArtistByIdAsync(int artistId)
            => _artistRepository.GetByIdAsync(artistId);
    }
}
