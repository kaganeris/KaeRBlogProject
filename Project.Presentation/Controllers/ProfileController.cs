using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.AppUserDTOs;
using Project.Application.Models.DTOs.AuthorDTOs;
using Project.Application.Services.Abstract;
using Project.Domain.Entities;
using System.Security.Claims;

namespace Project.Presentation.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IAppUserService userService;
        private readonly IAuthorService authorService;
        private readonly IMapper mapper;

        public ProfileController(UserManager<AppUser> userManager, IAppUserService userService, IAuthorService authorService, IMapper mapper)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.authorService = authorService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIDClaim == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string userID = userIDClaim.Value;

            AppUser currentUser = await userManager.FindByIdAsync(userID);
            if (currentUser.FirstName == null)
            {
                UpdateUserDetailDTO updateUserDetailDTO = new UpdateUserDetailDTO();
                updateUserDetailDTO.Id = currentUser.Id;
                return RedirectToAction("UpdateUser", updateUserDetailDTO);
            }
            else
            {
                return View(currentUser);
            }

        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(Guid Id)
        {
            AppUser appUser = await userManager.FindByIdAsync(Id.ToString());
            if (appUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                UpdateUserDetailDTO updateUserDetailDTO = mapper.Map<UpdateUserDetailDTO>(appUser);
                return View(updateUserDetailDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDetailDTO updateUserDetailDTO)
        {
            if (ModelState.IsValid)
            {
                await userService.UpdateUserDetail(updateUserDetailDTO);
                return RedirectToAction("Index");
            }
            else
            {
                return View(updateUserDetailDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> BeAuthor(Guid appUserId)
        {
            AppUser appUser = await userManager.FindByIdAsync(appUserId.ToString());
            if (appUser == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                IdentityResult result = await userManager.AddToRoleAsync(appUser, "Author");
                if (result.Succeeded)
                {
                    await userManager.RemoveFromRoleAsync(appUser, "User");
                    CreateAuthorDTO createAuthorDTO = new CreateAuthorDTO();
                    createAuthorDTO.AppUserId = appUserId;
                    await authorService.CreateAuthor(createAuthorDTO);
                    return RedirectToAction("Logout", "User");
                }
                else
                    return RedirectToAction("Index");
            }
        }

    }
}
