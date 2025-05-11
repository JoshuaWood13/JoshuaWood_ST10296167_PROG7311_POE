
using JoshuaWood_ST10296167_PROG7311_POE.Models;
using JoshuaWood_ST10296167_PROG7311_POE.Repository.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JoshuaWood_ST10296167_PROG7311_POE.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<Models.User> _signInManager;

        // Controller
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public UserService(IUserRepository userRepository, SignInManager<Models.User> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        
        // Service Methods
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public async Task<bool> LoginUserAsync(Login login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email,login.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IdentityResult> RegisterFarmerAsync(Register register)
        {
            var farmer = new Models.User
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.Email,
                Email = register.Email,
                EmailConfirmed = true,
                FarmerCode = register.FarmerCode
            };

            return await _userRepository.CreateFarmerAsync(farmer, register.Password);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> GenerateNewFarmerCode()
        {
            var latestFarmerCode = await _userRepository.GetLatestFarmerCodeAsync();

            if (string.IsNullOrEmpty(latestFarmerCode))
            {
                return "F001";
            }

            // Used ChatGPT to get composite formatting code
            int codeNumber;
            var numericPart = latestFarmerCode.Substring(1); // Remove the "F" from the start
            if (int.TryParse(numericPart, out codeNumber))
            {
                codeNumber++; 
            }
            else
            {
                codeNumber = 1; 
            }

            return $"F{codeNumber:D3}"; 
        }
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//