using Microsoft.AspNetCore.Mvc;

namespace FrontendByDotnteMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            return View();
        }
    }
}
