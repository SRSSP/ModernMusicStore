using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MusicStore.Web.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var session = HttpContext.Session;
            var cartId = session.GetString("CartId");
            if (string.IsNullOrEmpty(cartId))
            {
                cartId = Guid.NewGuid().ToString();
                session.SetString("CartId", cartId);
            }

            var count = await _cartService.GetCartCountAsync(cartId);
            return View(count);
        }
    }
}
