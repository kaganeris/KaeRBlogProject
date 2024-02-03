using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.LikeDTOs;
using Project.Application.Models.DTOs.ReplyDTOs;
using Project.Application.Services.Abstract;

namespace Project.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LikeController : Controller
    {
        private readonly ILikeService likeService;

        public LikeController(ILikeService likeService)
        {
            this.likeService = likeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber)
        {
            List<LikeVM> likes = await likeService.GetAllLikes();
            ViewBag.TotalLikeCount = likes.Count;
            return View(likes.Skip((pageNumber * 10) - 10).Take(10).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await likeService.DeleteLike(id);
            if (result)
            {
                TempData["Delete"] = $"{id} ID'li Beğeni Başarıyla Silindi";
                return RedirectToAction("Index", "Like");
            }
            else
            {
                TempData["Delete"] = $"{id} ID'li Beğeni Silinemedi";
                return RedirectToAction("Index", "Like");
            }
        }
    }
}
