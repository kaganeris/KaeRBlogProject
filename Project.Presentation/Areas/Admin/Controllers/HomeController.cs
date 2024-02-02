using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.AppUserDTOs;
using Project.Application.Services.Abstract;

namespace Project.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IAppUserService appUserService;
        private readonly IPostService postService;

        public HomeController(IAppUserService appUserService,IPostService postService)
        {
            this.appUserService = appUserService;
            this.postService = postService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAllUsers(int pageNumber)
        {
            List<UserDTO> userDTOs = await appUserService.GetAllUsers(pageNumber);

            ViewBag.TotalUserCount = userDTOs.Count;
            return PartialView("_HomeTablePartial",userDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPostCount()
        {
            int postCount = await postService.GetPostCount();

            ViewBag.PostCount = postCount;
            return PartialView("_HomePostCountPartial");
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersCount()
        {
            List<UserDTO> userDTOs = await appUserService.UserCount();

            UserCountDTO userCountDTO = new UserCountDTO();
            userCountDTO.TotalUserCount = userDTOs.Count;
            int authorCount = 0;
            foreach (var item in userDTOs)
            {
                if(item.UserType != 0)
                {
                    authorCount++;
                }
            }
            userCountDTO.AuthorCount = authorCount;
            userCountDTO.NormalUserCount = userDTOs.Count - authorCount;

            return PartialView("_HomeUserCountPartial",userCountDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAdminInfos()
        {
            UserDTO userDTO = await appUserService.GetAdminInfo();

            return Json(userDTO);
        }
    }
}
