using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.Entities;

namespace MusicStore.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserAsync(string username);
    }
}

namespace MusicStore.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(MusicStoreDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(string username)
        {
            return await _dbSet
                .Include(o => o.OrderDetails)
                .Where(o => o.Username == username)
                .ToListAsync();
        }
    }
}
