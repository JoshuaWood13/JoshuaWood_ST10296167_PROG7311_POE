using System.Security.Claims;

namespace JoshuaWood_ST10296167_PROG7311_POE.Services.Product
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(Models.Product product);

        Task<string> GenerateProductCodeAsync();
    }
}
