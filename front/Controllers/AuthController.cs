using front.Dtos.Auth;
using front.Services;
using Microsoft.AspNetCore.Mvc;

namespace front.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService Service;

        public AuthController(AuthService service)
        {
            Service = service;
        }

        public ActionResult Login()
        {
            string token = HttpContext.Session.GetString("token");

            if (token != null)
            {
                return RedirectToAction("Index", "Producto");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto login)
        {
            string token = await Service.Login(login);

            if (token == null)
            {
                return View();
            }
            HttpContext.Session.SetString("token", token);
            return RedirectToAction("Index", "Producto");
        }

        public async Task<ActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

        public ActionResult Signup()
        {
            string token = HttpContext.Session.GetString("token");

            if (token != null)
            {
                return RedirectToAction("Index", "Producto");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Signup(SignUpDto signup)
        {
            try
            {
                if (!await Service.Signup(signup))
                {
                    return View();
                }
                return RedirectToAction("Login", "Auth");
            }
            catch
            {
                return View();
            }
        }
    }
}
