using Microsoft.AspNetCore.Mvc;
using MusicStore.Application.Interfaces;

namespace MusicStore.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IAlbumService _albumService;

        public StoreController(IGenreService genreService, IAlbumService albumService)
        {
            _genreService = genreService;
            _albumService = albumService;
        }

        // GET: /Store
        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.GetGenresAsync();
            return View(genres);
        }

        // GET: /Store/Browse/5
        // The route passes the id segment (e.g. /Store/Browse/1) so bind to 'id' to ensure model binding works.
        public async Task<IActionResult> Browse(int id)
        {
            // Load the genre (includes its Albums collection) because the view expects a Genre model
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        // GET: /Store/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);
            if (album == null)
                return NotFound();
            return View(album);
        }
    }
}
