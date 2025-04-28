using JoshuaWood_ST10296167_PROG7311_POE.Data;
using Microsoft.EntityFrameworkCore;

namespace JoshuaWood_ST10296167_PROG7311_POE.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        //Controller
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        public async Task<Models.User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//