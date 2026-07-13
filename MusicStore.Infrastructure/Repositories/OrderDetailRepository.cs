using MusicStore.Domain.Entities;

namespace MusicStore.Infrastructure.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
    }
}

namespace MusicStore.Infrastructure.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(MusicStoreDbContext context) : base(context) { }
    }
}
