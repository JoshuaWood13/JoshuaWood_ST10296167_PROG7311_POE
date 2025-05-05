using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace JoshuaWood_ST10296167_PROG7311_POE.Repository.Product
{
    public interface IProductRepository
    {
        Task<bool> createProduct(Models.Product product);

        Task<string> GetLatestProductCodeAsync();

        Task<List<Models.Product>> GetAllProductsByFarmerCodeAsync(string farmerCode);
    }
}
