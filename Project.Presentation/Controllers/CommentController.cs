using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.CommentDTOs;
using Project.Application.Models.VMs.PostVMs;
using Project.Application.Services.Abstract;
using Project.Domain.Entities;
using System.Security.Claims;

namespace Project.Presentation.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly UserManager<AppUser> userManager;

        public CommentController(ICommentService commentService,UserManager<AppUser> userManager)
        {
            this.commentService = commentService;
            this.userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDTO createCommentDTO)
        {
            if(ModelState.IsValid)
            {
                var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIDClaim == null)
                {
                    return Json("Hata");
                }
                else
                {
                    string userID = userIDClaim.Value;
                    AppUser user = await userManager.GetUserAsync(User);
                    if (user.FirstName == null)
                    {
                        CommentErrorDto commentErrorDto = new CommentErrorDto();
                        commentErrorDto.Error = "Error";
                        commentErrorDto.userID = user.Id;
                        return Json(commentErrorDto);
                    }
                    createCommentDTO.AppUserId = Guid.Parse(userID);
                    bool result = await commentService.CreateComment(createCommentDTO);
                    if (result)
                    {
                        CommentDTO commentDTO = new CommentDTO();
                        commentDTO.Content = createCommentDTO.Content;
                        AppUser appUser = await userManager.GetUserAsync(User);
                        commentDTO.AppUserFullName = appUser.FullName;
                        commentDTO.CreatedDate = DateTime.Now;
                        commentDTO.AppUserImagePath = appUser.ImagePath;
                        return PartialView("_CommentPartial", commentDTO);
                    }
                    else
                        return Json("Hata");
                }
                
                
            }
            return Json("Hata");
        }
    }
}
