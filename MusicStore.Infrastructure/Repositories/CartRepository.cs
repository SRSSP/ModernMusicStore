using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MusicStore.Domain.Entities;

namespace MusicStore.Infrastructure.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<IEnumerable<Cart>> GetCartItemsAsync(string cartId);
        Task AddToCartAsync(string cartId, int albumId);
        Task RemoveFromCartAsync(string cartId, int recordId);
        Task<int> GetCartCountAsync(string cartId);
        Task EmptyCartAsync(string cartId);
        Task MigrateCartAsync(string anonCartId, string userName);
    }
}


namespace MusicStore.Infrastructure.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(MusicStoreDbContext context) : base(context) { }

        public async Task<IEnumerable<Cart>> GetCartItemsAsync(string cartId)
        {
            return await _dbSet
                .Include(c => c.Album)
                .Where(c => c.CartId == cartId)
                .ToListAsync();
        }

        public async Task AddToCartAsync(string cartId, int albumId)
        {
            var cartItem = await _dbSet.FirstOrDefaultAsync(c => c.CartId == cartId && c.AlbumId == albumId);
            if (cartItem != null)
            {
                cartItem.Count++;
                _dbSet.Update(cartItem);
            }
            else
            {
                await _dbSet.AddAsync(new Cart
                {
                    CartId = cartId,
                    AlbumId = albumId,
                    Count = 1,
                    DateCreated = System.DateTime.UtcNow
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(string cartId, int recordId)
        {
            var cartItem = await _dbSet.FirstOrDefaultAsync(c => c.CartId == cartId && c.RecordId == recordId);
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    _dbSet.Update(cartItem);
                }
                else
                {
                    _dbSet.Remove(cartItem);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCartCountAsync(string cartId)
        {
            return await _dbSet.Where(c => c.CartId == cartId).SumAsync(c => c.Count);
        }

        public async Task EmptyCartAsync(string cartId)
        {
            var items = _dbSet.Where(c => c.CartId == cartId);
            _dbSet.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task MigrateCartAsync(string anonCartId, string userName)
        {
            // Get anonymous cart items
            var anonItems = await _dbSet.Where(c => c.CartId == anonCartId).ToListAsync();
            if (!anonItems.Any())
                return;

            foreach (var anon in anonItems)
            {
                // If user already has the same album in cart, increment its count
                var existing = await _dbSet.FirstOrDefaultAsync(c => c.CartId == userName && c.AlbumId == anon.AlbumId);
                if (existing != null)
                {
                    existing.Count += anon.Count;
                    _dbSet.Update(existing);
                    _dbSet.Remove(anon);
                }
                else
                {
                    anon.CartId = userName;
                    _dbSet.Update(anon);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
