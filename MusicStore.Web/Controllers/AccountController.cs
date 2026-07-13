using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Application.Interfaces;
using MusicStore.Models;
using System.Security.Claims;

namespace MusicStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICartService? _cart_service; // inject via ctor
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(IUserService userService, ICartService cartService, SignInManager<IdentityUser> signInManager)
        {
            _userService = userService;
            _cart_service = cartService;
            _signInManager = signInManager;
        }

        private async Task MigrateShoppingCartAsync(string userName)
        {
            // Retrieve anonymous cart id (session/cookie)
            var anonCartId = HttpContext.Session.GetString("CartId") ?? Request.Cookies["CartId"];
            if (string.IsNullOrEmpty(anonCartId) || _cart_service == null)
                return;

            // Migrate items from the anonymous cart id to the authenticated user
            await _cart_service.MigrateCartAsync(anonCartId, userName);

            // Store the user name in session under the same cart key so subsequent
            // cart operations use the authenticated user's id (matches legacy behavior)
            HttpContext.Session.SetString("CartId", userName);
        }
        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Models.RegisterModel model)
        {
            if (ModelState.IsValid) 
            {
                var success = await _userService.RegisterUserAsync(model.UserName, model.Password, model.Email);
                if (success)
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError("", "Registration failed.");
            }
            return View(model);
        }

        // GET: /Account/Login
        public IActionResult LogOn()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOn(Models.LogOnModel model, string returnUrl)
        {
            // Some requests may include a returnUrl field that isn't part of the LogOnModel
            // and can produce a spurious required-model error. Remove it from ModelState
            // so validation focuses on the actual LogOnModel properties.
            ModelState.Remove("returnUrl");
            ModelState.Remove("ReturnUrl");

            if (!ModelState.IsValid)
                return View(model);

            // Use Identity SignInManager to validate credentials and sign in
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(model);
            }

            // Migrate anonymous cart items to the authenticated user
            await MigrateShoppingCartAsync(model.UserName);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // Sign out Identity cookie
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
