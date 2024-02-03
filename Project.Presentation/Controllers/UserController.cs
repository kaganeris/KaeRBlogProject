using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Helper;
using Project.Application.Models.DTOs.AppUserDTOs;
using Project.Application.Models.DTOs.ConfirmMailDTOs;
using Project.Application.Models.VMs.AppUserVMs;
using Project.Application.Services.Abstract;
using Project.Application.Validations;
using Project.Domain.Entities;

namespace Project.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IAppUserService appUserService;
        private readonly UserManager<AppUser> userManager;

        public UserController(IAppUserService appUserService,UserManager<AppUser> userManager)
        {
            this.appUserService = appUserService;
            this.userManager = userManager;
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
                {
                    ViewBag.Hata = "Kullanıcı adı veya Şifre hatalı!";
                    return View();
                }
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
                TempData["Success"] = "Hesap başarıyla aktifleştirildi.";
                return RedirectToAction("Login");
            }
            TempData["Mail"] = confirmMailDTO.Email;
            return View();
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(forgetPasswordVM.Mail);
                if (user == null)
                {
                    ViewBag.Uyarı = "Kayıtlı E-mail bulunamadı!";
                    return View();
                }
                string passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetTokenLink = Url.Action("ResetPassword", "User", new
                {
                    userId = user.Id,
                    token = passwordResetToken
                }, HttpContext.Request.Scheme);

                MailHelper.SendPasswordMail(forgetPasswordVM.Mail, passwordResetTokenLink);
                ViewBag.Tamamlandı = "Şifre yenileme bağlantısı başarıyla iletildi!";
                return View();
            }
            ViewBag.Uyarı = "Kayıtlı E-mail bulunamadı!";
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string userID, string token)
        {
            TempData["userID"] = userID;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            var userID = TempData["userID"];
            var token = TempData["token"];

            if (userID == null || token == null)
            {
                ViewBag.Uyarı = "Hata!";
                return View();
            }

            ResetPasswordVMValidator validator = new ResetPasswordVMValidator();
            var valid = validator.Validate(resetPasswordVM);
            if (valid.IsValid)
            {
                var user = await userManager.FindByIdAsync(userID.ToString());

                var result = await userManager.ResetPasswordAsync(user, token.ToString(), resetPasswordVM.Password);

                if (result.Succeeded)
                {
                    TempData["Success"] = "Şifre değiştirme işlemi başarılı!";
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("Hata", item.Description);
                    }
                    TempData["userID"] = userID;
                    TempData["token"] = token;
                    return View();
                }
            }
            foreach (var item in valid.Errors)
            {
                ModelState.AddModelError("Hata", item.ErrorMessage);
            }
            TempData["userID"] = userID;
            TempData["token"] = token;
            return View();
        }
    }
}
