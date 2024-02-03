using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.GenreDTOs;
using Project.Application.Models.DTOs.PostDTOs;
using Project.Application.Models.VMs.GenreVMs;
using Project.Application.Services.Abstract;

namespace Project.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber)
        {
            List<GenreVM> genreVm = await genreService.GetAllGenreList();
            ViewBag.TotalGenreCount = genreVm.Count;
            return View(genreVm.Skip((pageNumber * 10) - 10).Take(10).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreDTO createGenreDTO)
        {
            if(ModelState.IsValid)
            {
                bool result = await genreService.CreateGenre(createGenreDTO);
                if(result)
                {
                    TempData["UpdateSuccess"] = $"{createGenreDTO.Name} İsimli Kategori Başarıyla Oluşturuldu.";
                    return RedirectToAction("Index", "Genre");
                }
                else
                {
                    return View(createGenreDTO);
                }
            }
            else
            {
              return View(createGenreDTO);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            UpdateGenreDTO updateGenre = await genreService.GetGenreById(id);
            return View(updateGenre);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateGenreDTO updateGenreDTO)
        {
            if (ModelState.IsValid)
            {
                bool result = await genreService.UpdateGenre(updateGenreDTO);
                if (result)
                {
                    TempData["UpdateSuccess"] = $"{updateGenreDTO.Id} ID'li Kategori Başarıyla Güncellendi";
                    return RedirectToAction("Index", "Genre");
                }
                else
                {
                    return View(updateGenreDTO);
                }
            }
            else
            {
                return View(updateGenreDTO);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await genreService.DeleteGenre(id);
            if (result)
            {
                TempData["Delete"] = $"{id} ID'li Kategori Başarıyla Silindi";
                return RedirectToAction("Index", "Genre");
            }
            else
            {
                TempData["Delete"] = $"{id} ID'li Kategori Silinemedi";
                return RedirectToAction("Index", "Genre");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Active(int id)
        {
            bool result = await genreService.Active(id);
            if (result)
            {
                TempData["Active"] = $"{id} ID'li Kategori Başarıyla Aktifleştirildi";
                return RedirectToAction("Index", "Genre");
            }
            else
            {
                TempData["Active"] = $"{id} ID'li Kategori Aktifleştirilemedi";
                return RedirectToAction("Index", "Genre");
            }
        }
    }
}
