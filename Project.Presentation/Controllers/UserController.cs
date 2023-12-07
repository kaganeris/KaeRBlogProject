using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.AppUserDTOs;
using Project.Application.Models.DTOs.ConfirmMailDTOs;
using Project.Application.Services.Abstract;

namespace Project.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IAppUserService appUserService;

        public UserController(IAppUserService appUserService)
        {
            this.appUserService = appUserService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await appUserService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await appUserService.Login(loginDTO);

                if (result.IsNotAllowed)
                {
                    TempData["Mail"] = await appUserService.GetUserEmail(loginDTO);
                    return RedirectToAction("ConfirmMail");
                }
                else if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                    return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await appUserService.Register(registerDTO);
                if (result.Succeeded)
                {
                    TempData["Mail"] = registerDTO.Email;
                    return RedirectToAction("ConfirmMail");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("Hata", item.Description);
                    }
                    return View();
                }
            }
            else
                return View();
        }

        [HttpGet]
        public IActionResult ConfirmMail()
        {
            var value = TempData["Mail"];
            ViewBag.Email = value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmMail(ConfirmMailDTO confirmMailDTO)
        {
            bool result = await appUserService.EmailConfirm(confirmMailDTO);
            if (result == true)
            {
                return RedirectToAction("Login");
            }
            TempData["Mail"] = confirmMailDTO.Email;
            return View();
        }
    }
}
