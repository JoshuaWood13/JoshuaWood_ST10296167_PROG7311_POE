
using JoshuaWood_ST10296167_PROG7311_POE.Models;
using JoshuaWood_ST10296167_PROG7311_POE.Repository.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JoshuaWood_ST10296167_PROG7311_POE.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        // Controller
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        // Methods
        //------------------------------------------------------------------------------------------------------------------------------------------//
        // This method adds a product with the current time
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

        // This method generates, formats and returns a new product code for a product
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

        // This method returns all the products added by a specific farmer
        public async Task<List<Models.Product>> GetProductsByFarmerAsync(string farmerCode)
        {
            var products = await _productRepository.GetAllProductsByFarmerCodeAsync(farmerCode);
            return products;
        }

        // This method returns all products added by all farmers
        public async Task<List<Models.Product>> GetProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return products;
        }

        // This method dynamically gets a list of products based on selected filters 
        public async Task<List<Models.Product>> GetFilteredProductsAsync(FilteredProducts filter)
        {
            var products = await _productRepository.GetAllProductsAsync();
            var filterQuery = products.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SelectedFarmerCode))
            {
                filterQuery = filterQuery.Where(p => p.FarmerCode == filter.SelectedFarmerCode);
            }

            if (!string.IsNullOrEmpty(filter.SelectedCategory))
            {
                filterQuery = filterQuery.Where(p => p.Category == filter.SelectedCategory);
            }

            if (filter.StartDate.HasValue)
            {
                filterQuery = filterQuery.Where(d => d.DateAdded >= filter.StartDate.Value);
            }

            if (filter.EndDate.HasValue)
            {
                var nextDay = filter.EndDate.Value.AddDays(1);
                filterQuery = filterQuery.Where(d => d.DateAdded < nextDay);
            }

            return filterQuery.ToList();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//