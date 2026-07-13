using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.Entities;

namespace MusicStore.Infrastructure.Repositories
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<IEnumerable<Album>> GetByGenreAsync(int genreId);
    }
}

namespace MusicStore.Infrastructure.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(MusicStoreDbContext context) : base(context) { }

        public async Task<IEnumerable<Album>> GetByGenreAsync(int genreId)
        {
            return await _dbSet
                .Where(a => a.GenreId == genreId)
                .ToListAsync();
        }

        public override async Task<Album?> GetByIdAsync(int id)
        {
            // Include navigation properties so callers (views/controllers) have Genre and Artist loaded
            return await _dbSet
                .Include(a => a.Genre)
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(a => a.AlbumId == id);
        }
    }
}
