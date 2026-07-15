using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStore.Application.Interfaces;
using MusicStore.Domain.Entities;

namespace MusicStore.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IGenreService _genreService;
        private readonly IArtistService _artistService;
        private readonly ILogger<StoreManagerController> _logger;

        public StoreManagerController(
            IAlbumService albumService,
            IGenreService genreService,
            IArtistService artistService,
            ILogger<StoreManagerController> logger)
        {
            _albumService = albumService;
            _genreService = genreService;
            _artistService = artistService;
            _logger = logger;
        }

        // Populate ViewBag entries used by Create/Edit views. The views expect
        // ViewBag.GenreId and ViewBag.ArtistId (SelectList) as well as the
        // original ViewBag.Genres/ViewBag.Artists for compatibility.
        private async Task PopulateGenresAndArtistsAsync(int? selectedGenreId = null, int? selectedArtistId = null)
        {
            var genres = await _genreService.GetGenresAsync();
            ViewBag.Genres = genres;
            ViewBag.GenreId = new SelectList(genres, "GenreId", "Name", selectedGenreId);

            var artists = await _artistService.GetArtistsAsync();
            ViewBag.Artists = artists;
            ViewBag.ArtistId = new SelectList(artists, "ArtistId", "Name", selectedArtistId);
        }

        // GET: /StoreManager
        public async Task<IActionResult> Index()
        {
            var albums = await _albumService.GetAlbumsAsync();
            return View(albums);
        }

        // GET: /StoreManager/Create
        public async Task<IActionResult> Create()
        {
            await PopulateGenresAndArtistsAsync();
            return View();
        }

        // POST: /StoreManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Album album)
        {
            if (ModelState.IsValid)
            {
                await _albumService.AddAlbumAsync(album);
                return RedirectToAction("Index");
            }
            await PopulateGenresAndArtistsAsync(album.GenreId, album.ArtistId);
            return View(album);
        }

        // GET: /StoreManager/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);
            if (album == null)
                return NotFound();
            await PopulateGenresAndArtistsAsync(album.GenreId, album.ArtistId);
            return View(album);
        }

        // POST: /StoreManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                await _albumService.UpdateAlbumAsync(album);
                return RedirectToAction("Index");
            }

            await PopulateGenresAndArtistsAsync(album.GenreId, album.ArtistId);
            return View(album);
        }

        // GET: /StoreManager/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var album = await _albumService.GetAlbumByIdAsync(id.Value);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: /StoreManager/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);
            if (album == null)
                return NotFound();
            return View(album);
        }

        // POST: /StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Assume AlbumService has DeleteAsync (add if missing)
            await _albumService.DeleteAlbumAsync(id);
            return RedirectToAction("Index");
        }
    }
}
