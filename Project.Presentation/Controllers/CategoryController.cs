using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.VMs.GenreVMs;
using Project.Application.Services.Abstract;

namespace Project.Presentation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IGenreService genreService;

        public CategoryController(IGenreService genreService)
        {
            this.genreService = genreService;
        }
        public async Task<IActionResult> Index(string categoryName,string returnUrl)
        {
            GenreVM genreVM = await genreService.GetGenreByName(categoryName);
            if (genreVM == null)
            {
                return Redirect(returnUrl);
            }
            return View(genreVM);
        }
    }
}
