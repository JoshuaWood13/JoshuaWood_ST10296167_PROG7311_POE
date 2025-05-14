using JoshuaWood_ST10296167_PROG7311_POE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JoshuaWood_ST10296167_PROG7311_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        // Controller
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        // Views
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public async Task<IActionResult> Index()
        {
            // Check if user is authenticated
            if (User?.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            var user = await _userManager.GetUserAsync(User);

            // If user is null, sign them out
            if (user == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login", "User");
            }

            // Add user info for use in Index view
            ViewData["FirstName"] = user.FirstName ?? "User";
            ViewData["IsEmployee"] = await _userManager.IsInRoleAsync(user, "Employee");
            ViewData["IsFarmer"] = await _userManager.IsInRoleAsync(user, "Farmer");

            return View();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//