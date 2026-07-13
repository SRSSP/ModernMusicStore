using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.Entities;

namespace MusicStore.Infrastructure.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
    }
}

namespace MusicStore.Infrastructure.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(MusicStoreDbContext context) : base(context) { }
        public override async Task<Genre?> GetByIdAsync(int id)
        {
            // Include Albums so the view can iterate Model.Albums without additional queries
            return await _dbSet
                .Include(g => g.Albums)
                .FirstOrDefaultAsync(g => g.GenreId == id);
        }
    }
}
