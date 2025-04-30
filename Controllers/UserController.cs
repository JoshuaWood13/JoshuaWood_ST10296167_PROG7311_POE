using JoshuaWood_ST10296167_PROG7311_POE.Models;
using JoshuaWood_ST10296167_PROG7311_POE.Services.Login;
using Microsoft.AspNetCore.Mvc;

namespace JoshuaWood_ST10296167_PROG7311_POE.Controllers
{
    public class UserController : Controller
    {
        private readonly ILoginService _loginService;

        // Controller
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public UserController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        // Views
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public IActionResult Login()
        {
            return View();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        public async Task<IActionResult> Logout()
        {
            await _loginService.LogoutAsync();
            return RedirectToAction("Login","User");
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        [HttpPost]
        public async Task<IActionResult> LoginUser(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            var isValidUser = await _loginService.LoginUserAsync(login);

            if (isValidUser)
            {
                return RedirectToAction("Index","Home");
            }
            TempData["Error"] = "Incorrect email or password";
            ModelState.Clear();
            return View("Login");
        }
    }
}
