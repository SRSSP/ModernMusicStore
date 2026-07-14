using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Application.Interfaces;
using MusicStore.Domain.Entities;

namespace MusicStore.Web.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private const string PromoCode = "FREE";
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public CheckoutController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        // GET: /Checkout
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Checkout/AddressAndPayment
        [HttpGet]
        public IActionResult AddressAndPayment()
        {
            // Display the address and payment form
            return View();
        }

        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressAndPayment([FromForm] Order order, [FromForm] string PromoCode)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
               .Where(x => x.Value.Errors.Count > 0)
               .Select(x => new { Field = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage) });

                foreach (var error in errors)
                    System.Diagnostics.Debug.WriteLine($"Field: {error.Field} → {string.Join(", ", error.Errors)}");

                return View(order);
            }

            try
            {
                if (!string.Equals(PromoCode, CheckoutController.PromoCode, StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("PromoCode", "Invalid promo code.");
                    return View(order);
                }

                order.Username = User?.Identity?.Name ?? "";
                order.OrderDate = DateTime.UtcNow;

                var cartId = GetCartId();
                var created = await _orderService.CreateOrderAsync(order, cartId);

                return RedirectToAction("Complete", new { id = created.OrderId });
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your order.");
                return View(order);
            }
        }

        // POST: /Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Order order)
        {
            if (ModelState.IsValid)
            {
                var cartId = GetCartId();
                var createdOrder = await _orderService.CreateOrderAsync(order, cartId);
                return RedirectToAction("Complete", new { id = createdOrder.OrderId });
            }
            return View(order);
        }

        // GET: /Checkout/Complete/5
        public async Task<IActionResult> Complete(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            // The Complete view expects an int (order id) as its model.
            // Pass the OrderId so the view can render the confirmation shown in the legacy UI.
            return View(order.OrderId);
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
