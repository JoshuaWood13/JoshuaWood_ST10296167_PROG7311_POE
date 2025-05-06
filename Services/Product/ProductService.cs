
using JoshuaWood_ST10296167_PROG7311_POE.Repository.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JoshuaWood_ST10296167_PROG7311_POE.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly UserManager<Models.User> _userManager;

        // Controller
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public ProductService(IProductRepository productRepository, UserManager<Models.User> userManager)
        {
            _productRepository = productRepository;
            _userManager = userManager;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        // Methods
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public async Task<bool> AddProductAsync(Models.Product product)
        {
            try
            {
                product.DateAdded = DateTime.Now;

                return await _productRepository.createProduct(product);
            }
            catch
            {
                return (false);
            }
        }

        public async Task<string> GenerateProductCodeAsync()
        {
            var latestCode = await _productRepository.GetLatestProductCodeAsync();

            if (string.IsNullOrEmpty(latestCode))
            {
                return "P001";
            }

            int codeNumber;
            var numericPart = latestCode.Substring(1); // Remove the "P" from the start
            if (int.TryParse(numericPart, out codeNumber))
            {
                codeNumber++;
            }
            else
            {
                codeNumber = 1;
            }

            return $"P{codeNumber:D3}"; 
        }

        public async Task<List<Models.Product>> GetProductsByFarmerAsync(string farmerCode)
        {
            var products = await _productRepository.GetAllProductsByFarmerCodeAsync(farmerCode);
            return products;
        }

        public async Task<List<Models.Product>> GetProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return products;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

    }
}
