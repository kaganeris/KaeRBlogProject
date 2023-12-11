using Microsoft.AspNetCore.Mvc;

namespace Project.Presentation.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
