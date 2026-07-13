using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Domain.Entities;

namespace MusicStore.Application.Interfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(string cartId, int albumId);
        Task RemoveFromCartAsync(string cartId, int recordId);
        Task<IEnumerable<Cart>> GetCartItemsAsync(string cartId);
        Task<int> GetCartCountAsync(string cartId);
        Task EmptyCartAsync(string cartId);
        Task MigrateCartAsync(string anonCartId, string userName);
    }
}
