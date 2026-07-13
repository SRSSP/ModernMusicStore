using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Application.Interfaces;
using MusicStore.Domain.Entities;
using MusicStore.Infrastructure.Repositories;

namespace MusicStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order, string cartId)
        {
            // Transfer cart items to order details, clear cart, etc.
            var cartItems = await _cartRepository.GetCartItemsAsync(cartId);
            foreach (var item in cartItems)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    Quantity = item.Count,
                    UnitPrice = item.Album.Price
                });
            }
            order.Total = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice);
            await _orderRepository.AddAsync(order);
            await _cartRepository.EmptyCartAsync(cartId);
            return order;
        }

        public Task<IEnumerable<Order>> GetOrdersByUserAsync(string username)
            => _orderRepository.GetOrdersByUserAsync(username);

        public Task<Order?> GetOrderByIdAsync(int orderId)
            => _orderRepository.GetByIdAsync(orderId);
    }
}
