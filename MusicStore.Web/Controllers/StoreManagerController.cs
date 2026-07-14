using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public StoreManagerController(
            IAlbumService albumService,
            IGenreService genreService,
            IArtistService artistService)
        {
            _albumService = albumService;
            _genreService = genreService;
            _artistService = artistService;
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
            ViewBag.Genres = await _genreService.GetGenresAsync();
            ViewBag.Artists = await _artistService.GetArtistsAsync();
            return View();
        }

        // POST: /StoreManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Album album)
        {
            if (ModelState.IsValid)
            {
                // Assume AlbumService has AddAsync (add if missing)
                await _albumService.AddAlbumAsync(album);
                return RedirectToAction("Index");
            }
            ViewBag.Genres = await _genreService.GetGenresAsync();
            ViewBag.Artists = await _artistService.GetArtistsAsync();
            return View(album);
        }

        // GET: /StoreManager/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);
            if (album == null)
                return NotFound();
            ViewBag.Genres = await _genreService.GetGenresAsync();
            ViewBag.Artists = await _artistService.GetArtistsAsync();
            return View(album);
        }

        // POST: /StoreManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                // Assume AlbumService has UpdateAsync (add if missing)
                await _albumService.UpdateAlbumAsync(album);
                return RedirectToAction("Index");
            }
            ViewBag.Genres = await _genreService.GetGenresAsync();
            ViewBag.Artists = await _artistService.GetArtistsAsync();
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
