using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.CommentDTOs;
using Project.Application.Models.DTOs.ReplyDTOs;
using Project.Application.Services.Abstract;
using Project.Domain.Entities;
using System.Security.Claims;

namespace Project.Presentation.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IReplyService replyService;
        private readonly UserManager<AppUser> userManager;

        public ReplyController(IReplyService replyService,UserManager<AppUser> userManager)
        {
            this.replyService = replyService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Create(CreateReplyDTO createReplyDTO)
        {
            if (ModelState.IsValid)
            {
                var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIDClaim == null)
                {
                    return Json("Hata");
                }
                else
                {
                    string userID = userIDClaim.Value;
                    createReplyDTO.AppUserId = Guid.Parse(userID);
                    bool result = await replyService.CreateReply(createReplyDTO);
                    if (result)
                    {
                        ReplyDTO replyDTO = new ReplyDTO();
                        replyDTO.Content = createReplyDTO.Content;
                        AppUser appUser = await userManager.GetUserAsync(User);
                        replyDTO.AppUserFullName = appUser.FullName;
                        replyDTO.CreatedDate = DateTime.Now;
                        return PartialView("_ReplyPartial", replyDTO);
                    }
                    else
                        return Json("Hata");
                }


            }
            return Json("Hata");
        }
    }
}
