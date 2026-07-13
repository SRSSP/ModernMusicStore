using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Application.Interfaces;

namespace MusicStore.Web.ViewComponents
{
    public class GenreMenuViewComponent : ViewComponent
    {
        private readonly IGenreService _genreService;

        public GenreMenuViewComponent(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _genreService.GetGenresAsync();
            return View(genres);
        }
    }
}
