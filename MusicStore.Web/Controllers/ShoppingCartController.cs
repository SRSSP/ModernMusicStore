using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MusicStore.Application.Interfaces;

namespace MusicStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IAlbumService _albumService;

        public ShoppingCartController(ICartService cartService, IAlbumService albumService)
        {
            _cartService = cartService;
            _albumService = albumService;
        }

        // GET: /ShoppingCart
        public async Task<IActionResult> Index()
        {
            var cartId = GetCartId();
            var items = (await _cartService.GetCartItemsAsync(cartId)).ToList();
            var count = await _cartService.GetCartCountAsync(cartId);
            ViewBag.CartCount = count;

            // Build a view model expected by the view
            var vm = new MvcMusicStore.ViewModels.ShoppingCartViewModel
            {
                CartItems = items,
                CartTotal = items.Sum(i => (i.Album?.Price ?? 0m) * i.Count)
            };

            return View(vm);
        }

        // POST: /ShoppingCart/AddToCart/5
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var cartId = GetCartId();
            await _cartService.AddToCartAsync(cartId, id);
            return RedirectToAction("Index");
        }

        // POST: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int recordId)
        {
            var cartId = GetCartId();
            await _cartService.RemoveFromCartAsync(cartId, recordId);
            return RedirectToAction("Index");
        }

        // POST: /ShoppingCart/EmptyCart
        [HttpPost]
        public async Task<IActionResult> EmptyCart()
        {
            var cartId = GetCartId();
            await _cartService.EmptyCartAsync(cartId);
            return RedirectToAction("Index");
        }

        // GET: /ShoppingCart/CartSummary
        // In ASP.NET Core there is no [ChildActionOnly]. Prefer returning a partial view
        // or implementing a ViewComponent. This action returns a partial view for the cart summary.
        [HttpGet]
        public async Task<IActionResult> CartSummary()
        {
            var cartId = GetCartId();       
            var count = await _cartService.GetCartCountAsync(cartId);
            ViewData["CartCount"] = count;

            // Return the partial view named CartSummary located under Views/ShoppingCart/CartSummary.cshtml
            return PartialView("CartSummary");
        }


        private string GetCartId()
        {
            if (HttpContext.Session.GetString("CartId") is string cartId)
                return cartId;

            cartId = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("CartId", cartId);
            return cartId;
        }
    }
}
