using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Application.Interfaces;
using MusicStore.Domain.Entities;
using MusicStore.Infrastructure.Repositories;

namespace MusicStore.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public Task<IEnumerable<Genre>> GetGenresAsync()
            => _genreRepository.GetAllAsync();

        public Task<Genre?> GetGenreByIdAsync(int genreId)
            => _genreRepository.GetByIdAsync(genreId);

        public Task<Genre?> GetGenreByNameAsync(string genre)
            => _genreRepository.GetGenreByNameAsync(genre);
    }
}
