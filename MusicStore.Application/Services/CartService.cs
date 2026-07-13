using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Application.Interfaces;
using MusicStore.Domain.Entities;
using MusicStore.Infrastructure.Repositories;

namespace MusicStore.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Task AddToCartAsync(string cartId, int albumId)
            => _cartRepository.AddToCartAsync(cartId, albumId);

        public Task RemoveFromCartAsync(string cartId, int recordId)
            => _cartRepository.RemoveFromCartAsync(cartId, recordId);

        public Task<IEnumerable<Cart>> GetCartItemsAsync(string cartId)
            => _cartRepository.GetCartItemsAsync(cartId);

        public Task<int> GetCartCountAsync(string cartId)
            => _cartRepository.GetCartCountAsync(cartId);

        public Task EmptyCartAsync(string cartId)
            => _cartRepository.EmptyCartAsync(cartId);
        public Task MigrateCartAsync(string anonCartId, string userName)
            => _cartRepository.MigrateCartAsync(anonCartId, userName);
    }
}
