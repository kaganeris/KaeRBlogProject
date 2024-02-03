using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.CommentDTOs;
using Project.Application.Models.VMs.GenreVMs;
using Project.Application.Services.Abstract;

namespace Project.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber)
        {
            List<CommentVM> comments = await commentService.GetAllCommentList();
            ViewBag.TotalCommentCount = comments.Count;
            return View(comments.Skip((pageNumber * 10) - 10).Take(10).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await commentService.DeleteComment(id);
            if (result)
            {
                TempData["Delete"] = $"{id} ID'li Yorum Başarıyla Silindi";
                return RedirectToAction("Index", "Comment");
            }
            else
            {
                TempData["Delete"] = $"{id} ID'li Yorum Silinemedi";
                return RedirectToAction("Index", "Comment");
            }
        }
    }
}
