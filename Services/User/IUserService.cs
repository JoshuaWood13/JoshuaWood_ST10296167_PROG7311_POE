using JoshuaWood_ST10296167_PROG7311_POE.Models;
using Microsoft.AspNetCore.Identity;

namespace JoshuaWood_ST10296167_PROG7311_POE.Services.User
{
    public interface IUserService
    {
        Task<bool> LoginUserAsync(Models.Login login);

        Task<IdentityResult> RegisterFarmerAsync(Register register);

        Task LogoutAsync();

        Task<string> GenerateNewFarmerCode();
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//