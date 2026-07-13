using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Domain.Entities;

namespace MusicStore.Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetGenresAsync();
        Task<Genre?> GetGenreByIdAsync(int genreId);
    }
}
