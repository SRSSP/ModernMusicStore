using MusicStore.Domain.Entities;

namespace MusicStore.Infrastructure.Repositories
{
    public interface IArtistRepository : IRepository<Artist>
    {
    }
}

namespace MusicStore.Infrastructure.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(MusicStoreDbContext context) : base(context) { }
    }
}
