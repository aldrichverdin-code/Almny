using Microsoft.AspNetCore.Mvc;

namespace Almyweb.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        // GET: Auth/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Aquí va la lógica de autenticación
            // Por ahora solo redirige al Home
            return RedirectToAction("Index", "Home");
        }

        // POST: Auth/Register
        [HttpPost]
        public IActionResult Register(string email, string password, string fullName)
        {
            // Aquí va la lógica de registro
            // Por ahora solo redirige al Login
            return RedirectToAction("Login", "Auth");
        }

        // POST: Auth/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            // Aquí va la lógica de cierre de sesión
            return RedirectToAction("Index", "Home");
        }
    }
}