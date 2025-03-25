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

        public ActionResult Signup()
        {
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
