using JoshuaWood_ST10296167_PROG7311_POE.Models;
using Microsoft.AspNetCore.Identity;

namespace JoshuaWood_ST10296167_PROG7311_POE.Repository.User
{
    public interface IUserRepository
    {
        Task<Models.User> GetUserByEmailAsync(string email);

        Task<IdentityResult> CreateFarmerAsync(Models.User farmer, string password);

        Task<string?> GetLatestFarmerCodeAsync();
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//