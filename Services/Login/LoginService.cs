
using JoshuaWood_ST10296167_PROG7311_POE.Models;
using JoshuaWood_ST10296167_PROG7311_POE.Repository.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JoshuaWood_ST10296167_PROG7311_POE.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        // Controller
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public LoginService(IUserRepository userRepository, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        
        public async Task<bool> LoginUserAsync(Models.Login login)
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
        //------------------------------------------------------------------------------------------------------------------------------------------//

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//