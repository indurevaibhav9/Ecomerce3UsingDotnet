using FrontendByDotnteMVC.Models;
using FrontendByDotnteMVC.Services;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;

namespace FrontendByDotnteMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View(new RegisterDto());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var success = await _authService.RegisterAsync(model);
            if (success)
                return RedirectToAction("Login");
            ModelState.AddModelError("", "Registration failed. Please try again.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = await _authService.LoginAsync(model.UserName, model.Password);

            if (token == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View(model);
            }

            // Store token in session
            HttpContext.Session.SetString("JWTToken", token);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWTToken");
            return RedirectToAction("Login");
        }
    }
}
