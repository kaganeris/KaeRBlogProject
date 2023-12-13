using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.LikeDTOs;
using Project.Application.Services.Abstract;
using System.Security.Claims;

namespace Project.Presentation.Controllers
{
    public class LikeController : Controller
    {
        private readonly ILikeService likeService;

        public LikeController(ILikeService likeService)
        {
            this.likeService = likeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLikeDTO createLikeDTO)
        {
            if (ModelState.IsValid)
            {
                var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                string userID = userIDClaim.Value;
                createLikeDTO.AppUserId = Guid.Parse(userID);
                bool result = await likeService.CreateLike(createLikeDTO);
                if (result)
                {
                    return Json("Ok");
                }
                else
                    return Json("Hata");
            }
            else
                return Json("Hata");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(HardDeleteLikeDTO hardDeleteLikeDTO)
        {
            if (ModelState.IsValid)
            {
                var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                string userID = userIDClaim.Value;
                hardDeleteLikeDTO.AppUserId = Guid.Parse(userID);
                int likeId = await likeService.GetLikeId(hardDeleteLikeDTO.PostId, hardDeleteLikeDTO.AppUserId);
                if (likeId > 0)
                {
                    bool result = await likeService.DeleteLike(likeId);
                    if (result)
                    {
                        return Json("Ok");
                    }
                    else
                        return Json("Hata");
                }
                else
                    return Json("Hata");
               
            }
            else
                return Json("Hata");
        }
    }
}
