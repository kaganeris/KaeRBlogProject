using Microsoft.AspNetCore.Mvc;
using Project.Application.Helper;
using Project.Application.Models.DTOs.AppUserDTOs;
using Project.Application.Validations;
using Project.Presentation.Models;
using System.Diagnostics;

namespace Project.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactUsDTO contactUsDTO)
        {
            ContactUsDTOValidator validator = new ContactUsDTOValidator();
            var valid = validator.Validate(contactUsDTO);
            if (valid.IsValid)
            {
                MailHelper.SendContact(contactUsDTO);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var item in valid.Errors)
                {
                    ModelState.AddModelError("ContactUsHata", item.ErrorMessage);
                }
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}