﻿using JoshuaWood_ST10296167_PROG7311_POE.Models;
using JoshuaWood_ST10296167_PROG7311_POE.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JoshuaWood_ST10296167_PROG7311_POE.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        // Controller
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        // Views
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Register()
        {
            var newFarmerCode = await _userService.GenerateNewFarmerCode(); 
            var model = new Register { FarmerCode = newFarmerCode };

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Login","User");
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        // Methods
        //------------------------------------------------------------------------------------------------------------------------------------------//
        [HttpPost]
        public async Task<IActionResult> LoginUser(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", login);
            }

            // Call user service login method
            var isValidUser = await _userService.LoginUserAsync(login);

            if (isValidUser)
            {
                return RedirectToAction("Index","Home");
            }
            TempData["Error"] = "Incorrect email or password";
            return View("Login",login);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterFarmer(Register farmer)
        {
            if(!ModelState.IsValid)
            {
                return View("Register", farmer);
            }

            // Call user service register method
            var result = await _userService.RegisterFarmerAsync(farmer);

            if (result.Succeeded)
            {
                TempData["Success"] = "Farmer succesfully created!";
                return RedirectToAction("Register");
            }

            // Display only valid error messages
            foreach (var error in result.Errors)
            {
                if (error.Code.Contains("Password"))
                {
                    ModelState.AddModelError("Password", error.Description);
                }
                else if (error.Code.Contains("Email") || error.Description.Contains("already taken"))
                {
                    ModelState.AddModelError("Email", "Email is already taken");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, error.Description); 
                }
            }

            return View("Register", farmer);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//