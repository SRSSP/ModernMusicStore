using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Domain.Entities;

namespace MusicStore.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order, string cartId);
        Task<IEnumerable<Order>> GetOrdersByUserAsync(string username);
        Task<Order?> GetOrderByIdAsync(int orderId);
    }
}
