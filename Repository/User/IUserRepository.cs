using JoshuaWood_ST10296167_PROG7311_POE.Models;

namespace JoshuaWood_ST10296167_PROG7311_POE.Repository.User
{
    public interface IUserRepository
    {
        Task<Models.User> GetUserByEmailAsync (string email);
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//