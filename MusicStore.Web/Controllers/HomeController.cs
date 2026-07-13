using Microsoft.AspNetCore.Mvc;
using MusicStore.Application.Interfaces;

namespace MusicStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAlbumService _albumService;

        public HomeController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        // GET: /
        public async Task<IActionResult> Index()
        {
            var albums = await _albumService.GetAlbumsAsync();
            return View(albums);
        }

        // GET: /Home/About
        public IActionResult About()
        {
            return View();
        }

        // GET: /Home/Contact
        public IActionResult Contact()
        {
            return View();
        }
    }
}
