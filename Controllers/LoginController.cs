using Microsoft.AspNetCore.Mvc;
using ResturangFrontEnd.Models;

namespace ResturangFrontEnd.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";

            return View();
        }

        [HttpPost] 
        public IActionResult Login(Login login)
        {
            ViewData["Title"] = "Login Post";

            string hardcodedUsername = "admin";
            string hardcodedPassword = "123";

            if (login.Username == hardcodedUsername && login.Password == hardcodedPassword)
            {
                return RedirectToAction("Welcome");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(login);
            }
        }

        public IActionResult Welcome()
        {
            ViewData["Title"] = "Welcome";
            return View();
        }
    }
}
