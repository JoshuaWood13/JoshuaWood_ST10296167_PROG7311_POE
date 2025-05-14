using JoshuaWood_ST10296167_PROG7311_POE.Data;
using JoshuaWood_ST10296167_PROG7311_POE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JoshuaWood_ST10296167_PROG7311_POE.Repository.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        // Controller 
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        // Methods
        //------------------------------------------------------------------------------------------------------------------------------------------//
        // This method adds a product to the db
        public async Task<bool> createProduct(Models.Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        // This method returns the latest product code from the db
        public async Task<string> GetLatestProductCodeAsync()
        {
            var latestCode = await _context.Products
                .OrderByDescending(p => p.ProductCode)
                .Select(p => p.ProductCode)
                .FirstOrDefaultAsync();

            return latestCode;
        }

        // This method gets all products created by a specific farmer from the db 
        public async Task<List<Models.Product>> GetAllProductsByFarmerCodeAsync(string farmerCode)
        {
            var products = await _context.Products.Where(p => p.FarmerCode == farmerCode).ToListAsync();
            return products;
        }

        // This method gets all the products from the db
        public async Task<List<Models.Product>> GetAllProductsAsync()
        {
            var productts = await _context.Products.ToListAsync();
            return productts;
        }
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//