using JoshuaWood_ST10296167_PROG7311_POE.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JoshuaWood_ST10296167_PROG7311_POE.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;

        //Controller
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public UserRepository(ApplicationDbContext context, UserManager<Models.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        
        // Methods
        //------------------------------------------------------------------------------------------------------------------------------------------//
        // This method retireves a user from the db based on their email
        public async Task<Models.User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        // This method adds a new farmer to the db
        public async Task<IdentityResult> CreateFarmerAsync(Models.User farmer, string password)
        {
            var result = await _userManager.CreateAsync(farmer, password);

            if(result.Succeeded)
            {
                Console.WriteLine("Farmer created!");
                await _userManager.AddToRoleAsync(farmer, "Farmer");
            }

            return result;
        }

        // This method retrieves the latest farmer code from the db
        public async Task<string?> GetLatestFarmerCodeAsync()
        {
            var farmers = await _userManager.GetUsersInRoleAsync("Farmer");

            var latestCode = farmers
                .Where(u => u.FarmerCode != null)
                .Select(u => u.FarmerCode)
                .OrderByDescending(code => code)
                .FirstOrDefault();
   
            return latestCode;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//