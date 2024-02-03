using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.ReplyDTOs;
using Project.Application.Services.Abstract;

namespace Project.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReplyController : Controller
    {
        private readonly IReplyService replyService;

        public ReplyController(IReplyService replyService)
        {
            this.replyService = replyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber)
        {
            List<ReplyVM> replies = await replyService.GetAllReplys();
            ViewBag.TotalReplyCount = replies.Count;
            return View(replies.Skip((pageNumber * 10) - 10).Take(10).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await replyService.DeleteReply(id);
            if (result)
            {
                TempData["Delete"] = $"{id} ID'li Yanıt Başarıyla Silindi";
                return RedirectToAction("Index", "Reply");
            }
            else
            {
                TempData["Delete"] = $"{id} ID'li Yanıt Silinemedi";
                return RedirectToAction("Index", "Reply");
            }
        }
    }
}
