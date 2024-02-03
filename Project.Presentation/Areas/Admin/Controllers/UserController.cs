using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.AppUserDTOs;
using Project.Application.Services.Abstract;
using Project.Domain.Entities;

namespace Project.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly IAppUserService userService;

        public UserController(UserManager<AppUser> userManager,IMapper mapper,IAppUserService userService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.userService = userService;
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
                TempData["UpdateSuccess"] = $"{updateUserDetailDTO.Id} ID'li Kullanıcı Başarıyla Güncellendi";
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View(updateUserDetailDTO);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            AppUser appUser = await userManager.FindByIdAsync(id.ToString());
            if (appUser == null)
            {
                IdentityResult result = await userManager.DeleteAsync(appUser);
                if (result.Succeeded)
                {
                    TempData["Delete"] = $"{appUser.FullName} İsimli Kullanıcı Başarıyla Silindi";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Passive(Guid Id)
        {
            AppUser appUser = await userManager.FindByIdAsync(Id.ToString());
            bool result = await userService.Passive(Id);
            if (result)
            {
                TempData["Delete"] = $"{appUser.FullName} İsimli Kullanıcı Başarıyla Pasifleştirildi";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Delete"] = $"{appUser.FullName} İsimli Kullanıcı Pasifleştirilemedi!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Active(Guid Id)
        {
            AppUser appUser = await userManager.FindByIdAsync(Id.ToString());
            bool result = await userService.Active(Id);
            if (result)
            {
                TempData["Active"] = $"{appUser.FullName} İsimli Kullanıcı Başarıyla Aktifleştirildi";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Active"] = $"{appUser.FullName} İsimli Kullanıcı Aktifleştirilemedi!";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
