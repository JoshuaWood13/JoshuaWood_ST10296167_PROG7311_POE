
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
       // private readonly UserManager<Models.User> _userManager;

        // Controller
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public UserService(IUserRepository userRepository, SignInManager<Models.User> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            //_userManager = userManager;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        
        // Service Methods
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public async Task<bool> LoginUserAsync(Login login)
        {
            var user = await _userRepository.GetUserByEmailAsync(login.Email);

            if (user != null)
            {
                var isPasswordValid = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

                if (isPasswordValid.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return true;
                }
            }
            return false;
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